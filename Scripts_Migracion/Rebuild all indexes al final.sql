SET NOCOUNT ON;
 
DECLARE  @Version                           [numeric] (18, 10)
        ,@SQLStatementID                    [int]
        ,@CurrentTSQLToExecute              [nvarchar](max)
        ,@FillFactor                        [int]        = 100 -- Change if needed
        ,@PadIndex                          [varchar](3) = N'OFF' -- Change if needed
        ,@SortInTempDB                      [varchar](3) = N'OFF' -- Change if needed
        ,@OnlineRebuild                     [varchar](3) = N'OFF' -- Change if needed
        ,@LOBCompaction                     [varchar](3) = N'ON' -- Change if needed
        ,@DataCompression                   [varchar](4) = N'NONE' -- Change if needed
        ,@MaxDOP                            [int]        = NULL -- Change if needed
        ,@IncludeDataCompressionArgument    [char](1);
 
IF OBJECT_ID(N'TempDb.dbo.#Work_To_Do') IS NOT NULL
    DROP TABLE #Work_To_Do
CREATE TABLE #Work_To_Do
    (
      [sql_id] [int] IDENTITY(1, 1)
                     PRIMARY KEY ,
      [tsql_text] [varchar](1024) ,
      [completed] [bit]
    )
 
SET @Version = CAST(LEFT(CAST(SERVERPROPERTY(N'ProductVersion') AS [nvarchar](128)), CHARINDEX('.', CAST(SERVERPROPERTY(N'ProductVersion') AS [nvarchar](128))) - 1) + N'.' + REPLACE(RIGHT(CAST(SERVERPROPERTY(N'ProductVersion') AS [nvarchar](128)), LEN(CAST(SERVERPROPERTY(N'ProductVersion') AS [nvarchar](128))) - CHARINDEX('.', CAST(SERVERPROPERTY(N'ProductVersion') AS [nvarchar](128)))), N'.', N'') AS [numeric](18, 10))
 
IF @DataCompression IN (N'PAGE', N'ROW', N'NONE')
    AND (
        @Version >= 10.0
        AND SERVERPROPERTY(N'EngineEdition') = 3
        )
BEGIN
    SET @IncludeDataCompressionArgument = N'Y'
END
 
IF @IncludeDataCompressionArgument IS NULL
BEGIN
    SET @IncludeDataCompressionArgument = N'N'
END
 
INSERT INTO #Work_To_Do ([tsql_text], [completed])
SELECT 'ALTER INDEX [' + i.[name] + '] ON' + SPACE(1) + QUOTENAME(t2.[TABLE_CATALOG]) + '.' + QUOTENAME(t2.[TABLE_SCHEMA]) + '.' + QUOTENAME(t2.[TABLE_NAME]) + SPACE(1) + 'REBUILD WITH (' + SPACE(1) + + CASE
        WHEN @PadIndex IS NULL
            THEN 'PAD_INDEX =' + SPACE(1) + CASE i.[is_padded]
                    WHEN 1
                        THEN 'ON'
                    WHEN 0
                        THEN 'OFF'
                    END
        ELSE 'PAD_INDEX =' + SPACE(1) + @PadIndex
        END + CASE
        WHEN @FillFactor IS NULL
            THEN ', FILLFACTOR =' + SPACE(1) + CONVERT([varchar](3), REPLACE(i.[fill_factor], 0, 100))
        ELSE ', FILLFACTOR =' + SPACE(1) + CONVERT([varchar](3), @FillFactor)
        END + CASE
        WHEN @SortInTempDB IS NULL
            THEN ''
        ELSE ', SORT_IN_TEMPDB =' + SPACE(1) + @SortInTempDB
        END + CASE
        WHEN @OnlineRebuild IS NULL
            THEN ''
        ELSE ', ONLINE =' + SPACE(1) + @OnlineRebuild
        END + ', STATISTICS_NORECOMPUTE =' + SPACE(1) + CASE st.[no_recompute]
        WHEN 0
            THEN 'OFF'
        WHEN 1
            THEN 'ON'
        END + ', ALLOW_ROW_LOCKS =' + SPACE(1) + CASE i.[allow_row_locks]
        WHEN 0
            THEN 'OFF'
        WHEN 1
            THEN 'ON'
        END + ', ALLOW_PAGE_LOCKS =' + SPACE(1) + CASE i.[allow_page_locks]
        WHEN 0
            THEN 'OFF'
        WHEN 1
            THEN 'ON'
        END + CASE
        WHEN @IncludeDataCompressionArgument = N'Y'
            THEN CASE
                    WHEN @DataCompression IS NULL
                        THEN ''
                    ELSE ', DATA_COMPRESSION =' + SPACE(1) + @DataCompression
                    END
        ELSE ''
        END + CASE
        WHEN @MaxDop IS NULL
            THEN ''
        ELSE ', MAXDOP =' + SPACE(1) + CONVERT([varchar](2), @MaxDOP)
        END + SPACE(1) + ')'
    ,0
FROM [sys].[tables] t1
INNER JOIN [sys].[indexes] i ON t1.[object_id] = i.[object_id]
    AND i.[index_id] > 0
    AND i.[type] IN (1, 2)
INNER JOIN [INFORMATION_SCHEMA].[TABLES] t2 ON t1.[name] = t2.[TABLE_NAME]
    AND t2.[TABLE_TYPE] = 'BASE TABLE'
INNER JOIN [sys].[stats] AS st WITH (NOLOCK) ON st.[object_id] = t1.[object_id]
    AND st.[name] = i.[name]
 
SELECT @SQLStatementID = MIN([sql_id])
FROM #Work_To_Do
WHERE [completed] = 0
 
WHILE @SQLStatementID IS NOT NULL
BEGIN
    SELECT @CurrentTSQLToExecute = [tsql_text]
    FROM #Work_To_Do
    WHERE [sql_id] = @SQLStatementID
 
    PRINT @CurrentTSQLToExecute
 
    EXEC [sys].[sp_executesql] @CurrentTSQLToExecute
 
    UPDATE #Work_To_Do
    SET [completed] = 1
    WHERE [sql_id] = @SQLStatementID
 
    SELECT @SQLStatementID = MIN([sql_id])
    FROM #Work_To_Do
    WHERE [completed] = 0
END