/*
This script was created by Visual Studio on 18/03/2019 at 2:46 PM.
Run this script on PHESL50298.CEMS_2 (sa) to make it the same as cems-dev.database.windows.net.CEMS (saadmin).
This script performs its actions in the following order:
1. Disable foreign-key constraints.
2. Perform DELETE commands. 
3. Perform UPDATE commands.
4. Perform INSERT commands.
5. Re-enable foreign-key constraints.
Please back up your target database before running this script.
*/

/*
Name: 
Date Created: 
Contents: 

*/
BEGIN
	DECLARE @major_version INT = 1;
	DECLARE @minor_version INT = 0;
	DECLARE @upgrade_version INT = 1; --increment from latest version
	IF NOT EXISTS(SELECT 1 FROM dbo.db_version dv WHERE dv.major_version  = 1 AND dv.minor_version = 0 AND dv.upgrade_version = @upgrade_version)
	BEGIN 
		BEGIN TRANSACTION
		BEGIN TRY
			-- start of seed data

			-- end of seed data
			INSERT INTO dbo.db_version ( major_version, minor_version, upgrade_version, applied_date )
			SELECT
			@major_version,
			@minor_version,
			@upgrade_version,GETDATE()
			
			COMMIT TRANSACTION
		END TRY
		BEGIN CATCH 
			IF @@TRANCOUNT > 0 
			ROLLBACK TRAN
		END CATCH 
	END
END
GO 
