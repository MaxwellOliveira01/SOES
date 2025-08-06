# SOE

SOE stands for "Simple Online Election". This project has a front and a backend to handle simple elections. Here you can:
- Create elections
- Register voters
- As a voter, authenticate and vote in your preferred party
- Count votes and reveal the winner

There's no way to "import" voters or elections, you need to add them directly into the database. You can use the script called "InitialData.sql" to fill this and test the application.

The aim of this project isn't to build the best interface — it's to show some simple ideas we can use to block a lot of different types of attacks on the electoral process.

## Happy path

The usual path for a voter is:
- Access the website and put its identifier on the form
- The system will send an OTP (One Time Password) to its email
- Put the OTP into the form and log into the system
- Choose an election and click to vote
- Choose one of the listed options and click to continue
- On the confirmation page, the browser will download an file before sending the vote
- After download, the vote is submitted and registered on system

Behind the scenes, there's a lot happening in these steps.

- The authentication with OTP works and every code can only be used once, within 2 minutes from its generation.
- The downloaded file is a private key in PEM format. The site generates a pair of RSA keys and creates a signature of the selected option. The public key and the signature are submitted to the server, but the private key is not! The idea is that the private key never travels on the network and only the voter has it.
- When the server receives the vote submission, there are a lot of checks (we'll explain later). The thing is: Before storing the vote in the database, the server creates another signature using its private key. The server's public key is in the database, so we can check this signature later, but the private key is only stored in memory and is lost when you turn off the system.

## Common attacks in an electoral system

There are a lot of different attacks that can be made on this system. The goal of every attack is to change the number of votes to give some advantage to a candidate or create a disadvantage for another. The goal of the system is to block these attacks and, if we can't block them, at least notice that it happened so we can cancel the election results. Now let's list some of them.

### Double vote submission

As a voter, I can authenticate in a bunch of browser tabs with the same account and try to submit a vote at the same time in every tab. I want to register more than one vote.

This is a simple attack that SOE can easily handle. When storing the vote, the system has an index to guarantee the connection of voter and election is unique. So, in that attack, one submission will work and the others will get an exception from the database.

### I'm admin and I want to ensure my candidate will win by manipulating the database

As an admin, I have full access to the code and database. So, I go to the database and do some "INSERT" commands to add more votes to my candidate. Doing that, I make sure my candidate will win the election.

This is a more complex attack where the attacker has way more privileges, but SOE can handle that. <b> It's not possible to add or edit votes </b> because of the server signature. Since the server private keys are only in memory, there's no way the attacker can get it, and they can't use other keys, because we check the server signature when counting the votes.

Note that this strategy works for creating and editing existing votes, since the signature will be invalid in both cases.

## I'm an admin and I will duplicate an existing vote

As an admin, I copy an existing vote a bunch of times, making everyone vote for my candidate.

By copying an existing vote, the signatures get copied too, so we can't detect it just by checking the signatures. But there are some strategies to defend ourselves. First, every voter has their own pair of keys generated before sending the vote, so it's simple to know that the copied signature is invalid using the public key.

But the admin has incredible powers! They can just go to the database and change the voter's public key to make sure the copied signature will be validated.

Yeah, the admin can do that, but... What about the server signature? They can copy it too to make sure it's valid. To handle that case, SOE uses two things: PSS padding, which adds some randomness, and database indexes to make sure the data in the server signature column is unique.

The PSS padding scheme gives us a good way to generate different signatures even if we use the same input, so naturally the generated signatures will be different. Because of that, the index on the server signature column won't be a problem in normal use, but it will prevent a malicious admin from doing this attack.

### So, is SOE the perfect online election system?

No, SOE isn't perfect. It can handle a lot of different attacks, even if they're made by a system admin, but the "simple" in the project name isn't random. There are a lot of other attacks and strategies to handle them that this system hasn't implemented. For example, an admin can just go to the servers table in the database, change the stored public key, change all votes, and recreate the signatures in a way the system won't detect. Also, this system doesn't detect when a vote is simply deleted from the database.

The idea is to show how simple ideas can handle a lot of different attacks. But for the complex ones, we need to add complexity to our implementation. Creating a perfect online election system is not an easy task.

### Project technologies

This project was made using .NET for the backend and Typescript (Angular) for the frontend. For databases, PostgreSQL was chosen.

### How to run

First, you'll need to run the frontend. Go to the `site` folder, run `npm i` and wait. After that, just run `ng serve` with Node 22 or greater. If everything goes ok, the frontend will be running on port 4200.

After that, you'll need to run the backend. Go to the `SOE` folder and run `dotnet run`. If everything goes well, the backend will be running on port 5173.

Now you can open your browser at http://localhost:4200/ and see the login page.

To log in, you can run the SQL script "InitialData.sql" in SOE/SQL—it adds some voters and elections to the database. Also, you need to configure an email to send the OTP codes in your dotnet secrets. In your secrets, add something like this:

```
{
  "SmtpConfig": {
    "From": "YOUR EMAIL HERE",
    "Server": "smtp.gmail.com",
    "Password": "YOUR EMAIL APPLICATION PASSWORD HERE"
  }
}
```