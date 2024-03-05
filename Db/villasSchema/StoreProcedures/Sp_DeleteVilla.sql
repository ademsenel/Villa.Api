CREATE PROCEDURE [villaSchema].[Sp_DeleteVilla]
	@Id int = 0
AS
	
	Set Xact_Abort On
	Set Nocount On

	Begin
		Delete 
		From [villaSchema].[Tbl_Villas] 
		where [Id] = @Id
	End

	Set Nocount Off

RETURN @Id
