CREATE PROCEDURE [villaSchema].[Sp_UpdateVilla]
	@Id            Int = null
    , @Name        Varchar(50) = null 
    , @Details     Varchar(100) = null
    , @Rate          Float(53) = null
    , @Sqft          Int = null
    , @Occupancy     Int = null
    , @ImageUrl      Varchar(200) = null
    , @Amenity       Varchar(50) = null
AS
	Set Xact_Abort On
	Set Nocount On

    Update [villaSchema].[Tbl_Villas] 
    Set [Name] = @Name   
        , [Details] = @Details
        , [Rate] = @Rate    
        , [Sqft] = @Sqft
        , [Occupancy] = @Occupancy
        , [ImageUrl] = @ImageUrl
        , [Amenity] = @Amenity
    Where [Id] = @Id

    Set Nocount Off;

RETURN @Id
