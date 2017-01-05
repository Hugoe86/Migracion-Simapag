-- ==================================================
--				  LISTADO 2 FACTURAS
-- ==================================================

SELECT DISTINCT rpu FROM Ope_Cor_Facturacion_Recibos WHERE Usuario_Creo = 'Migracion caso 5' OR Usuario_Creo = 'Migracion caso 6'

SELECT DISTINCT rpu FROM Ope_Cor_Facturacion_Recibos WHERE Usuario_Creo = 'Migracion caso 5'

SELECT DISTINCT rpu FROM Ope_Cor_Facturacion_Recibos WHERE Usuario_Creo = 'Migracion caso 6'
GO

-- ==================================================
--				PAGOS ANTICIPADOS Y OTROS
-- ==================================================

SELECT rpu,cuenta,nombre,panticipo FROM Simapag_09_01_2016.dbo.recibos 
WHERE ISNUMERIC(rpu)=1 AND foliorecib>0 AND sector<>99 AND panticipo > 0

SELECT rpu,cuenta,nombre,otros,recotros FROM Simapag_09_01_2016.dbo.recibos
WHERE ISNUMERIC(rpu)=1 AND foliorecib>0 AND sector<>99 AND (otros>0 OR recotros>0
GO

-- ==================================================
--			CUENTAS EN CEROS (SIN SUBSIDIO)
-- ==================================================

SELECT rpu,cuenta,nombre,tarifa,suspendido,total,subsidio FROM Simapag_09_01_2016.dbo.recibos 
WHERE ISNUMERIC(rpu)=1 AND foliorecib>0 AND sector<>99 AND total=0
GO


-- ==================================================
--				LISTA DE DATOS FISCALES
-- ==================================================



-- ==================================================
--				LISTA DE DATOS MIGRADOS
-- ==================================================

-- Bancos... Exacto
SELECT COUNT(*) FROM Cat_Cor_Bancos
SELECT COUNT(*) FROM Simapag_09_01_2016.dbo.bancos

-- Calles... Codigos y calles duplicados
SELECT COUNT(*) from Cat_Cor_Calles
SELECT COUNT(*) FROM Simapag_09_01_2016.dbo.calle WHERE calle<>''

-- Zonas... SICAP no tiene
SELECT COUNT(*) FROM CAT_COR_ZONAS

-- Tipos_Colonias... SICAP no tiene
SELECT COUNT(*) from Cat_Cor_Tipos_Colonias

-- Tipos_Viviendas... SICAP no tiene
SELECT COUNT(*) FROM CAT_COR_TIPOS_VIVIENDAS

-- Vialidades... SICAP no tiene
SELECT COUNT(*) FROM CAT_COR_VIALIDADES

-- Localidades... SICAP no tiene
SELECT COUNT(*) FROM Cat_Cor_Localidades

-- Materiales_Predios... SICAP no tiene
SELECT COUNT(*) FROM Cat_Cor_Materiales_Predios

-- Grupos_Dependencias... SICAP no tiene
SELECT count(*) FROM CAT_DEPENDENCIAS

-- Categoria_Conceptos_Cobros... SICAP no tiene
SELECT COUNT(*) FROM cat_cor_categoria_conceptos_cobros

-- Impuestos... SICAP no tiene
SELECT COUNT(*) FROM CAT_COM_IMPUESTOS

-- Tipos_Cuotas... SICAP no tiene
SELECT COUNT(*) FROM CAT_COR_TIPOS_CUOTAS

-- Marcas... Florencio agrego nueva marca
SELECT COUNT(*) FROM CAT_COM_MARCAS
SELECT count(*) FROM Simapag_09_01_2016.dbo.medidor

-- Motivos_No_Lectura... 5 registros vacios
SELECT COUNT(*) FROM Cat_Cor_Motivos_No_Lectura
SELECT COUNT(*) FROM Simapag_09_01_2016.dbo.anomalias WHERE anomalia<>''

-- Establecimientos... Solo establecimientos
SELECT count(*) from Cat_Cor_Establecimientos
SELECT count(*) FROM Simapag_09_01_2016.dbo.sucursales WHERE codsucursal<>'' AND codsucursal NOT LIKE 'C%' AND codsucursal NOT LIKE'D%'

-- Regiones... Se omiten sector 98 y 99
SELECT count(*) FROM Cat_Cor_Regiones
SELECT count(*) FROM Simapag_09_01_2016.dbo.sector WHERE codsector<>''

-- Poblaciones... 1 registro vacio
SELECT count(*) FROM Cat_cor_Poblacion
SELECT count(DISTINCT poblacion) FROM Simapag_09_01_2016.dbo.padron

-- Giros... SICAP no tiene
SELECT COUNT(*) FROM Cat_Cor_Giros

-- Productos... Se capturaron de un archivo de excel
SELECT COUNT(*) FROM CAT_COM_PRODUCTOS

-- Medidores... VA A CAMBIAR
SELECT COUNT(*) FROM Cat_Cor_Medidores

-- Dependencias... SICAP no tiene
SELECT COUNT(*) FROM CAT_DEPENDENCIAS

-- Giros_Actividades... Se unieron con giros
SELECT COUNT(*) FROM Cat_Cor_Giros_Actividades
SELECT COUNT(*) FROM Simapag_09_01_2016.dbo.actividad

-- Conceptos_Cobros... SICAP no tiene
SELECT COUNT(*) FROM Cat_Cor_Conceptos_Cobros

-- Grupos_Conceptos... SICAP no tiene
SELECT COUNT(*) FROM Cat_Cor_Grupos_Conceptos

-- Areas... SICAP no tiene
SELECT count(*) FROM CAT_AREAS

-- Roles... SICAP no tiene
SELECT COUNT(*) FROM APL_CAT_ROLES

-- Empleados... Se fueron agregando sobre la marcha
SELECT COUNT(*) FROM CAT_EMPLEADOS
SELECT COUNT(*) FROM Simapag_09_01_2016.dbo.lecturistas

-- Grupos_Conceptos_Detalles... SICAP no tiene
SELECT COUNT(*) FROM Cat_Cor_Grupos_Conceptos_Detalles

-- Departamentos... SICAP no tiene
SELECT COUNT(*) FROM CAT_COR_DEPARTAMENTOS

-- Distritos... SICAP no tiene
SELECT COUNT(*) FROM CAT_COR_DISTRITOS

-- Lecturistas...
SELECT COUNT(*) FROM Cat_Cor_Lecturistas
SELECT COUNT(*) FROM Simapag_09_01_2016.dbo.lecturistas

-- Rutas_Reparto... 
SELECT COUNT(*) FROM Cat_Cor_Rutas_Reparto
SELECT COUNT(*) FROM Cat_Cor_Rutas_Reparto

-- Brigadas... SICAP no tiene
SELECT COUNT(*) FROM Cat_Cor_Brigadas

-- Colonias... Simapag elimino 1 y se agrego otra generica
SELECT COUNT(*) FROM Cat_Cor_Colonias
SELECT COUNT(*) FROM Simapag_09_01_2016.dbo.colonia

-- Tipos_Fallas... Se tomo de un documento
SELECT COUNT(*) FROM CAT_COR_TIPOS_FALLAS

-- Calles_Colonias... SICAP no tiene
SELECT COUNT(*) FROM Cat_Cor_Calles_Colonias

-- Colonia_Poblacion... SICAP no tiene
SELECT COUNT(*) FROM Cat_Cor_Colonia_Poblacion

-- Tarifas... Simapag elimino las tarifss anteriores
SELECT COUNT(*) FROM Cat_Cor_Tarifas
SELECT COUNT(*) FROM Simapag_09_01_2016.dbo.tarifas

-- Usuarios... Se agregaron nuevos usuarios
SELECT COUNT(*) FROM Cat_Cor_Usuarios
SELECT COUNT(*) FROM Simapag_09_01_2016.dbo.padron

-- Predios_Medidores... Se rellenaron nulos
SELECT COUNT(*) FROM Cat_Cor_Predios_Medidores
SELECT COUNT(*) FROM Simapag_09_01_2016.dbo.padron WHERE nummedidor<>''

-- MEdidores_Lecturas_Detalles... Se agregaron las lecturas actuales
SELECT COUNT(*) FROM Cat_Cor_Medidores_Lecturas_Detalles
SELECT COUNT(*) FROM Simapag_09_01_2016.dbo.hislecturas

-- Ordenes_Trabajo... Se toman de ordenes pero se toman dos veces??
SELECT COUNT(*) FROM OPE_COR_ORDENES_TRABAJO
--SELECT COUNT(*) FROM Simapag_09_01_2016.dbo.ordenes

-- Convenios... 
SELECT COUNT(*) FROM Ope_Cor_Convenios
SELECT
	(SELECT COUNT(*) FROM Simapag_09_01_2016.dbo.convadmvo)+
	(SELECT count(*) FROM Simapag_09_01_2016.dbo.convpec)

-- Documentos_Acreditacion... SICAP no tiene
SELECT COUNT(*) FROM CAT_COR_DOCUMENTOS_ACREDITACION

-- Tipos_Propietarios... SICAP no tiene
SELECT COUNT(*) FROM CAT_COR_TIPOS_PROPIETARIOS

-- Diversos... Se tomaron los predios cortados
SELECT COUNT(*) FROM Ope_Cor_Diversos
SELECT COUNT(*) FROM Simapag_09_01_2016.dbo.recibos WHERE corte = 1

-- Facturacion_Recibos...
SELECT COUNT(*) FROM Ope_Cor_Facturacion_Recibos
SELECT
	(SELECT COUNT(*) FROM Simapag_09_01_2016.dbo.recibos) +
	(SELECT COUNT(*) FROM Simapag_09_01_2016.dbo.hisrecibos)


-- ==================================================
--			LISTA DE ELEMENTOS MIGRADOS
-- ==================================================

