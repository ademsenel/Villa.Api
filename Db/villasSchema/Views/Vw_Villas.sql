CREATE VIEW [villaSchema].[Vw_Villas]
	AS 
	SELECT 
	[Id]
    , [Name]
    , [Details]
    , [Rate]   
    , [Sqft]
    , [Occupancy]
    , [ImageUrl]
    , [Amenity]
    FROM [villaSchema].[Tbl_Villas]
