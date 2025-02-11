-- Elimina el procedimiento almacenado si ya existe
IF OBJECT_ID('dbo.sp_addcountry', 'P') IS NOT NULL
BEGIN
    DROP PROCEDURE dbo.sp_addcountry;
END
GO

-- Crea el procedimiento almacenado sp_addcountry
CREATE PROCEDURE dbo.sp_addcountry
    @Name VARCHAR(100),
    @Code CHAR(3)
AS
BEGIN
    SET NOCOUNT ON;

    -- Verifica si ya existe un país con el mismo nombre
    IF EXISTS (SELECT 1 FROM country WHERE name = @Name)
    BEGIN
        RAISERROR('Ya existe un país con el mismo nombre.', 16, 1);
        RETURN;
    END

    -- Verifica si ya existe un país con el mismo código
    IF EXISTS (SELECT 1 FROM country WHERE code = @Code)
    BEGIN
        RAISERROR('Ya existe un país con el mismo código.', 16, 1);
        RETURN;
    END

    -- Inserta el nuevo país
    INSERT INTO country (name, code)
    VALUES (@Name, @Code);
END
GO

-- Otorga permiso de ejecución a public
GRANT EXECUTE ON dbo.sp_addcountry TO public;
GO