DELETE FROM VoterElections;
DELETE FROM Otps;
DELETE FROM Voters;
DELETE FROM Servers;
DELETE FROM Options;
DELETE FROM Elections;

SELECT * FROM VoterElections;
SELECT * FROM Otps;
SELECT * FROM Servers;

INSERT INTO Voters (Id, Name, Email) VALUES
                                         (1, 'Maxwell Reis', 'max_soe_homolog@mailinator.com'),
                                         (2, 'Iasmim Queiroz', 'iasmim_soe_homolog@mailinator.com'),
                                         (3, 'Victor Hugo', 'victor_soe_homolog@mailinator.com');

SELECT * FROM Voters;

INSERT INTO Elections(Id, Name, Description) VALUES
                                                 (10, 'Eleição para o síndico do prédio', 'Sabendo que a dona Maria deixará o condomínio nos próximos dias, precisamos de um novo síndico.'),
                                                 (20, 'Quem é o melhor jogador de futebol do mundo?', 'Esse é o momento de decidir quem é o melhor jogador, faça sua escolha!'),
                                                 (30, 'Hoje é aniversário do Victor Hugo?', 'Sabendo que ele faz aniversário toda Segunda e Quarta desde o começo do semestre, será se hoje é o aniversário dele?'),
                                                 (40, 'Quantos anos tem Victor Hugo?', 'Sabendo que Victor Hugo faz aniversário duas vezes por semana há 3 meses, quantos anos ele tem?');


SELECT * FROM Elections;

INSERT INTO Options (Id, ElectionId, Name) VALUES
                                               (1001, 10, 'João da Silva'),
                                               (1002, 10, 'Maria da Silva'),
                                               (1003, 10, 'José da Silva'),
                                               (1004, 20, 'Cristiano Ronaldo'),
                                               (1005, 20, 'Lionel Messi'),
                                               (1006, 20, 'Neymar Jr.'),
                                               (1007, 30, 'Sim'),
                                               (1008, 30, 'Não'),
                                               (1009, 40, '20 anos'),
                                               (1010, 40, '21 anos'),
                                               (1011, 40, '22 anos'),
                                               (1012, 40, '23 anos'),
                                               (1013, 40, '24 anos'),
                                               (1014, 40, '25 anos');

SELECT * FROM Options;
