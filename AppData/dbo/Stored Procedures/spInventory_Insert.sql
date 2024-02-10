CREATE PROCEDURE [dbo].[spInventory_Insert]
	@ProductId INT,
	@Quantity INT,
	@PurchasePrice MONEY,
	@PurchasedDate datetime2

AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO dbo.Inventory (ProductId, Quantity, PurchasePrice, PurchasedDate)
	VALUES (@ProductId ,@Quantity ,@PurchasePrice, @PurchasedDate);
END
