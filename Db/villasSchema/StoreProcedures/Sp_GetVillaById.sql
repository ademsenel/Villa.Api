CREATE PROCEDURE [villaSchema].[Sp_GetVillaById]
	@Id int = 0
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
    Where [Id] = @Id

   	Set Nocount Off

RETURN 0
