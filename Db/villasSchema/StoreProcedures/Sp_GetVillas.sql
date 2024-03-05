CREATE PROCEDURE [villaSchema].[Sp_GetVillas]
AS
 	Set Xact_Abort On
	Set Nocount On

	SELECT 
	[Id]            
    , [Name]        
    , [Details]    
    , [Rate]          
    , [Sqft]         
    , [Occupancy]     
    , [ImageUrl]      
    , [Amenity]         
	FROM [villaSchema].[Vw_Villas]

   	Set Nocount Off

RETURN 0
