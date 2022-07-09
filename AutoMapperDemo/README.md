## This Demo is not finished yet!!

1: 
```sqlite
 SELECT "p"."PersonId", "p"."Age", "p"."CreatedUtc", "p"."FirstName", "p"."LastName", "p"."ModifiedUtc"
      FROM "Persons" AS "p"
      WHERE "p"."FirstName" = 'Bei'
```

2:
```sqlite
SELECT "p"."FirstName", "p"."LastName", "p"."Age", "a"."AddressId" IS NOT NULL, "a"."Country", "a"."Province", "a"."City", "a"."DetailAddress", "p"."PersonId", "a"."AddressId", "a0"."AccountId", "a0"."EmailAddress", "a0"."Password", "a0"."PersonId", "a0"."Username", "p"."CreatedUtc", "p"."ModifiedUtc" IS NOT NULL, "p"."ModifiedUtc"
      FROM "Persons" AS "p"
      LEFT JOIN "Addresses" AS "a" ON "p"."PersonId" = "a"."PersonId"
      LEFT JOIN "Accounts" AS "a0" ON "p"."PersonId" = "a0"."PersonId"
      ORDER BY "p"."PersonId", "a"."AddressId"

SELECT "p"."FirstName", "p"."LastName", "p"."Age", "a"."AddressId" IS NOT NULL, "a"."Country", "a"."Province", "a"."City", "a"."DetailAddress", "p"."PersonId", "a"."AddressId", "a0"."AccountId", "a0"."EmailAddress", "a0"."Password", "a0"."PersonId", "a0"."Username", "p"."CreatedUtc", "p"."ModifiedUtc" I       S NOT NULL, "p"."ModifiedUtc"
FROM "Persons" AS "p"
         LEFT JOIN "Addresses" AS "a" ON "p"."PersonId" = "a"."PersonId"
         LEFT JOIN "Accounts" AS "a0" ON "p"."PersonId" = "a0"."PersonId"
ORDER BY "p"."PersonId", "a"."AddressId"
```