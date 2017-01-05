-- ================================================================
--					TIPO DESCARGA ( PACHECO )
-- ================================================================

INSERT INTO
Cat_Cor_Tipo_Descarga(Tipo_Descarga,Estatus,Fecha_Creo,Usuario_Creo)
VALUES ('DESCARGA 6 PULGADAS','ACTIVO',GETDATE(),'SERGIO')
GO
INSERT INTO
Cat_Cor_Tipo_Descarga(Tipo_Descarga,Estatus,Fecha_Creo,Usuario_Creo)
VALUES ('DESCARGA 8 PULGADAS','ACTIVO',GETDATE(),'SERGIO')
GO
INSERT INTO
Cat_Cor_Tipo_Descarga(Tipo_Descarga,Estatus,Fecha_Creo,Usuario_Creo)
VALUES ('DESCARGA EN FUNCIONAMIENTO','ACTIVO',GETDATE(),'SERGIO')
GO
INSERT INTO
Cat_Cor_Tipo_Descarga(Tipo_Descarga,Estatus,Fecha_Creo,Usuario_Creo)
VALUES ('BIODIGESTOR','ACTIVO',GETDATE(),'SERGIO')
GO
INSERT INTO
Cat_Cor_Tipo_Descarga(Tipo_Descarga,Estatus,Fecha_Creo,Usuario_Creo)
VALUES ('MICRO PLANTA','ACTIVO',GETDATE(),'SERGIO')
GO
INSERT INTO
Cat_Cor_Tipo_Descarga(Tipo_Descarga,Estatus,Fecha_Creo,Usuario_Creo)
VALUES ('CARCAMO DE REBOMBEO PARA AGUAS
RESIDUALES','ACTIVO',GETDATE(),'SERGIO')
GO
INSERT INTO
Cat_Cor_Tipo_Descarga(Tipo_Descarga,Estatus,Fecha_Creo,Usuario_Creo)
VALUES ('CONVENIO NOTARIADO DE SERVIDUMBRE DE PASO PARA
DRENAJE','ACTIVO',GETDATE(),'SERGIO')
GO
INSERT INTO
Cat_Cor_Tipo_Descarga(Tipo_Descarga,Estatus,Fecha_Creo,Usuario_Creo)
VALUES ('FOSA SEPTICA','ACTIVO',GETDATE(),'SERGIO')
GO

INSERT INTO Cat_Cor_Tomas(Toma,Estatus,Fecha_Creo,Usuario_Creo)
VALUES ('BANQUETA PAVIMENTO','ACTIVO',GETDATE(),'SERGIO')
GO
INSERT INTO Cat_Cor_Tomas(Toma,Estatus,Fecha_Creo,Usuario_Creo)
VALUES ('BANQUETA TIERRA','ACTIVO',GETDATE(),'SERGIO')
GO
INSERT INTO Cat_Cor_Tomas(Toma,Estatus,Fecha_Creo,Usuario_Creo)
VALUES ('FOSA SEPTICA TOMA CORTA EN
PAVIMENTO','ACTIVO',GETDATE(),'SERGIO')
GO
INSERT INTO Cat_Cor_Tomas(Toma,Estatus,Fecha_Creo,Usuario_Creo)
VALUES ('FOSA SEPTICA TOMA CORTA EN
TERRACERIA','ACTIVO',GETDATE(),'SERGIO')
GO
INSERT INTO Cat_Cor_Tomas(Toma,Estatus,Fecha_Creo,Usuario_Creo)
VALUES ('FOSA SEPTICA TOMA LARGA EN
PAVIMENTO','ACTIVO',GETDATE(),'SERGIO')
GO

INSERT INTO Cat_Cor_Tomas(Toma,Estatus,Fecha_Creo,Usuario_Creo)
VALUES ('FOSA SEPTICA TOMA LARGA TERRACERIA','ACTIVO',GETDATE(),'SERGIO')
GO
INSERT INTO Cat_Cor_Tomas(Toma,Estatus,Fecha_Creo,Usuario_Creo)
VALUES ('FRACCIONAMIENTO','ACTIVO',GETDATE(),'SERGIO')
GO
INSERT INTO Cat_Cor_Tomas(Toma,Estatus,Fecha_Creo,Usuario_Creo)
VALUES ('PAVIMENTO SERVICIO INTEGRAL','ACTIVO',GETDATE(),'SERGIO')
GO
INSERT INTO Cat_Cor_Tomas(Toma,Estatus,Fecha_Creo,Usuario_Creo)
VALUES ('TIERRA SERVICIO INTEGRAL','ACTIVO',GETDATE(),'SERGIO')
GO
INSERT INTO Cat_Cor_Tomas(Toma,Estatus,Fecha_Creo,Usuario_Creo)
VALUES ('TOMA CORTA EN TERRACERIA','ACTIVO',GETDATE(),'SERGIO')
GO
INSERT INTO Cat_Cor_Tomas(Toma,Estatus,Fecha_Creo,Usuario_Creo)
VALUES ('TOMA CORTA PAVIMENTO','ACTIVO',GETDATE(),'SERGIO')
GO
INSERT INTO Cat_Cor_Tomas(Toma,Estatus,Fecha_Creo,Usuario_Creo)
VALUES ('TOMA LARGA EN TERRACERIA','ACTIVO',GETDATE(),'SERGIO')
GO

INSERT INTO Cat_Cor_Tomas(Toma,Estatus,Fecha_Creo,Usuario_Creo)
VALUES ('TOMA LARGA PAVIMENTO','ACTIVO',GETDATE(),'SERGIO')
GO


-- =======================================================
--				ACTUALIZACION 
-- =======================================================

--NOTA: Ya venian en el esqueleto de Simapag

ALTER table Ope_Cor_Diversos add Usuario_No_Registrado_ID int

alter table Ope_Cor_Diversos add constraint fk_diverso_usuario_no_registrado foreign key(Usuario_No_Registrado_ID) references
Cat_Cor_Usuarios_No_Registrados(Usuario_No_Registrado_ID)
