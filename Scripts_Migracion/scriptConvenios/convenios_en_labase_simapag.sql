

use Simapag_Praga

SELECT * FROM Simpag_09_10_2016.dbo.convadmvo WHERE convenio IN (3327)
SELECT * FROM Simpag_09_10_2016.dbo.pagadmvo WHERE convenio=3327
SELECT * FROM Simpag_09_10_2016.dbo.pagopagadmvo WHERE convenio=3327

SELECT * from Simapag_09_01_2016.dbo.convpec WHERE convenio=1006
SELECT * FROM Simapag_09_01_2016.dbo.pagpec WHERE convenio=1006
SELECT * FROM Simapag_09_01_2016.dbo.pagopagpec WHERE convenio=1006



SELECT Estatus,Tipo_Convenio,No_Convenio from Ope_Cor_Convenios WHERE Folio_Convenio=1006
SELECT * from Ope_Cor_Convenios_Detalles WHERE No_Convenio='0000002021'


SELECT top 1 * from Cat_Cor_Medidores
SELECT top 1 * FROM Cat_Cor_Predios_Medidores





