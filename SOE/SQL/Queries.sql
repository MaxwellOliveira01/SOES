UPDATE VoterElections
SET OptionId = (
    SELECT OptionId
    FROM VoterElections
    WHERE id = 10 -- <id_origem>
)
WHERE id = 11; -- <id_destino>

UPDATE VoterElections
SET Signature = (
    SELECT Signature
    FROM VoterElections
    WHERE id = 10 -- <id_origem>
)
WHERE id = 11; -- <id_destino>

UPDATE VoterElections
SET VoterPublicKey = (
    SELECT VoterPublicKey
    FROM VoterElections
    WHERE id = 10 -- <id_origem>
)
WHERE id = 11; -- <id_destino>

SELECT id, OptionId, Signature, VoterPublicKey FROM VoterElections;


/*
UPDATE VoterElections
SET ServerSignature = (
    SELECT ServerSignature
    FROM VoterElections
    WHERE id = 10 -- <id_origem>
)
WHERE id = 11; -- <id_destino>
*/
