CREATE PROCEDURE [villaSchema].[Sp_CreateVilla]
    @Id        Int = 0
    , @Name        Varchar(50) = null 
    , @Details     Varchar(500) = null
    , @Rate          Float (53) = 0
    , @Sqft          Int = 0
    , @Occupancy     Int = 0
    , @ImageUrl      Varchar(200) = null
    , @Amenity       Varchar(50) = null
AS
	Set Xact_Abort On
	Set Nocount On

    Insert 
    Into [villaSchema].[Tbl_Villas] (         
        [Name]        
        , [Details]    
        , [Rate]          
        , [Sqft]         
        , [Occupancy]     
        , [ImageUrl]      
        , [Amenity]         
        )
    Values (
        @Name
        , @Details
        , @Rate
        , @Sqft
        , @Occupancy
        , @ImageUrl
        , @Amenity  
    )

    Select @Id = SCOPE_IDENTITY();

    Set Nocount Off;

RETURN @Id
