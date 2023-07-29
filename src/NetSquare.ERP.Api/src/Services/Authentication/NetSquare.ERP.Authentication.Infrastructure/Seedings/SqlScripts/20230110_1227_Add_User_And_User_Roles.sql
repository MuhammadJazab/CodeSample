  --Users
  INSERT INTO [erp].[AspNetUsers] (Id, AccessFailedCount, ConcurrencyStamp, Email, EmailConfirmed, FirstName, LastName, LockoutEnabled,LockoutEnd, NormalizedEmail, NormalizedUserName, PasswordHash, PhoneNumber, PhoneNumberConfirmed, SecurityStamp, TwoFactorEnabled, UserName, CreatedBy, CreatedOn)
  VALUES('8e445865-a24d-4543-a6c6-9443d048cdb9', 0, '97b1c30c-ecb9-4f86-9822-1d24d049430f', 'admin@erp.com', 1, 'System', 'Admin', 0, null, 'ADMIN@ERP.COM', 'ADMIN@ERP.COM', 'AQAAAAEAACcQAAAAEKe5b8zVXX5olikxN36TxUwScou4SBrYNdqpapWad3UBLuFPHvFW8IF7QvbwiE2uTg==', null, 0, 'f6756f71-35e6-48ff-80f7-dd4b6340a200', 0, 'admin@erp.com', 'E26F1D8E-02A6-4ACC-BB5F-E715CFBF621D', GETDATE())
  INSERT INTO [erp].[AspNetUsers] (Id, AccessFailedCount, ConcurrencyStamp, Email, EmailConfirmed, FirstName, LastName, LockoutEnabled,LockoutEnd, NormalizedEmail, NormalizedUserName, PasswordHash, PhoneNumber, PhoneNumberConfirmed, SecurityStamp, TwoFactorEnabled, UserName, CreatedBy, CreatedOn  )
  VALUES('9e224968-33e4-4652-b7b7-8574d048cdb9', 0, 'f7f534a7-d56f-4766-ace3-6fc63f495585', 'user@erp.com', 1, 'System', 'User', 0, null, 'USER@ERP.COM', 'USER@ERP.COM', 'AQAAAAEAACcQAAAAEBlQ6gP1UwPhHyjGQw38SE6zqllyTUVpPLmqw/nr2riWt0Si2RGho/+ZGTNtu+w4pQ==', null, 0, '328399fc-0624-4e97-8f7d-8ec6515639f3', 0, 'user@erp.com', 'E26F1D8E-02A6-4ACC-BB5F-E715CFBF621D', GETDATE())


  --Roles
  INSERT INTO [erp].[AspNetRoles] (Id, ConcurrencyStamp, Name, NormalizedName)
  VALUES ('cac43a6e-f7bb-4448-baaf-1add431ccbbf', 'ee896caa-0be9-4e88-9bef-bd7a5f2a40df', 'User', 'USER')
    INSERT INTO [erp].[AspNetRoles] (Id, ConcurrencyStamp, Name, NormalizedName)
  VALUES ('cbc43a8e-f7bb-4445-baaf-1add431ffbbf', '7a971106-e3c7-47cd-8ed1-13bf4eac0c25', 'Admin', 'ADMIN')


  --UserRoles
  INSERT INTO [erp].[AspNetUserRoles](UserId, RoleId)
  VALUES ('8e445865-a24d-4543-a6c6-9443d048cdb9', 'cbc43a8e-f7bb-4445-baaf-1add431ffbbf')
  INSERT INTO [erp].[AspNetUserRoles](UserId, RoleId)
  VALUES ('9e224968-33e4-4652-b7b7-8574d048cdb9', 'cac43a6e-f7bb-4448-baaf-1add431ccbbf')