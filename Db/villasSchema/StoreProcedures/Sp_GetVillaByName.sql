CREATE PROCEDURE [villaSchema].[Sp_GetVillaByName]
	@Name varchar(50) = null
AS
 	Set Xact_Abort On
	Set Nocount On

   Select
	[Id]            
    , [Name]        
    , [Details]    
    , [Rate]          
    , [Sqft]         
    , [Occupancy]     
    , [ImageUrl]      
    , [Amenity]         
	FROM [villaSchema].[Vw_Villas]
    Where [Name] = @Name

   	Set Nocount Off

RETURN 0
