

USE Simapag_Roma

SELECT TOP 1 Adeudo,Total_Descuento, * FROM Ope_Cor_Convenios ORDER BY No_Convenio DESC
SELECT Total,Convenio_Desglosado_ID FROM Convenio_Desglosado WHERE No_Convenio='0000003312'

SELECT * FROM Convenio_Desglosado_Detalles WHERE Convenio_Desglosado_ID='0000003312'


SELECT Predio_ID FROM Cat_Cor_Predios WHERE RPU='000870302517'
SELECT * FROM Ope_Cor_Convenios WHERE Predio_ID='0000027255'

SELECT TOP 1 Adeudo,Total_Descuento, * FROM Ope_Cor_Convenios WHERE No_Convenio='0000003288'
SELECT Total,Convenio_Desglosado_ID FROM Convenio_Desglosado WHERE No_Convenio='0000003288'

SELECT SUM(Importe) FROM Convenio_Desglosado_Detalles WHERE Convenio_Desglosado_ID='0000003288'


