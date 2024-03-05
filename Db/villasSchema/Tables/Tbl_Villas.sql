CREATE TABLE [villaSchema].[Tbl_Villas]
(
	[Id]            Int Not Null Identity(1,1)
    , [Name]        Varchar(50) Not Null 
    , [Details]     Varchar(500) Null
    , [Rate]        Float (53) Not Null
    , [Sqft]        Int Not Null
    , [Occupancy]   Int Not Null
    , [ImageUrl]    Varchar(200) Not Null
    , [Amenity]     Varchar(50) Null    
    Constraint [Pk_Tbl_Villas_Id] Primary Key Clustered ([Id] Asc)
)
GO
