-- Elimina el procedimiento almacenado si ya existe
IF OBJECT_ID('dbo.sp_addweightcategory', 'P') IS NOT NULL
BEGIN
    DROP PROCEDURE dbo.sp_addweightcategory;
END
GO

-- Crea el procedimiento almacenado sp_addweightcategory
CREATE PROCEDURE dbo.sp_addweightcategory
    @Name VARCHAR(50),
    @MinWeight FLOAT,
    @MaxWeight FLOAT,
    @Gender CHAR(1)
AS
BEGIN
    SET NOCOUNT ON;

    -- Verifica si ya existe una categoría de peso con el mismo nombre
    IF EXISTS (SELECT 1 FROM weight_category WHERE name = @Name)
    BEGIN
        RAISERROR('Ya existe una categoría de peso con el mismo nombre.', 16, 1);
        RETURN;
    END

    -- Verifica si ya existe una categoría de peso con el mismo rango de peso y género
    IF EXISTS (SELECT 1 FROM weight_category WHERE min_weight = @MinWeight AND max_weight = @MaxWeight AND gender = @Gender)
    BEGIN
        RAISERROR('Ya existe una categoría de peso con el mismo rango de peso y género.', 16, 1);
        RETURN;
    END

    -- Inserta la nueva categoría de peso
    INSERT INTO weight_category (name, min_weight, max_weight, gender)
    VALUES (@Name, @MinWeight, @MaxWeight, @Gender);
END
GO

-- Otorga permiso de ejecución a public
GRANT EXECUTE ON dbo.sp_addweightcategory TO public;
GO