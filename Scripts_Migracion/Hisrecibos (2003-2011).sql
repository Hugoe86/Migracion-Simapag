INSERT INTO [Simapag_20161011].[dbo].[hisrecibos]
           ([foliorecib]
           ,[rpu]
           ,[cuenta]
           ,[nombre]
           ,[sector]
           ,[ruta]
           ,[calle]
           ,[colonia]
           ,[num_ext]
           ,[num_int]
           ,[reparto]
           ,[ordenlec]
           ,[actividad]
           ,[tarifa]
           ,[tienemedidor]
           ,[nummedidor]
           ,[rfc]
           ,[pensionado]
           ,[fecultpago]
           ,[suspendido]
           ,[estadocartera]
           ,[agua]
           ,[alcantarillado]
           ,[nvalec]
           ,[fnvalec]
           ,[annvalec]
           ,[lecactual]
           ,[flecactual]
           ,[anlecactua]
           ,[lecanterior]
           ,[flecanterior]
           ,[anlecanterior]
           ,[lecturisac]
           ,[consumo]
           ,[pagua]
           ,[palcan]
           ,[psanea]
           ,[rezagua]
           ,[recagua]
           ,[rezalcan]
           ,[recalcan]
           ,[rezsanea]
           ,[recsanea]
           ,[recotros]
           ,[crbomb]
           ,[otros]
           ,[iva]
           ,[suma]
           ,[lecturas]
           ,[importepago]
           ,[fechapago]
           ,[vencimient]
           ,[periodo]
           ,[ppagua]
           ,[ppalcan]
           ,[ppsanea]
           ,[prezagua]
           ,[prezalcan]
           ,[prezsanea]
           ,[precagua]
           ,[precalcan]
           ,[precsanea]
           ,[precotros]
           ,[pcrbomb]
           ,[potros]
           ,[piva]
           ,[ajustes]
           ,[bonificacion]
           ,[estado]
           ,[facturacion]
           ,[estimado])
           
     SELECT  [foliorecib]
      ,[rpu]
      ,[cuenta]
      ,[nombre]
      ,[sector]
      ,[ruta]
      ,[calle]
      ,[colonia]
      ,[num_ext]
      ,[num_int]
      ,[reparto]
      ,[ordenlec]
      ,[actividad]
      ,[tarifa]
      ,[tienemedid]
      ,[nummedidor]
      ,[rfc]
      ,[pensionado]
      ,[fecultpago]
      ,[suspendido]
      ,[estadocart]
      ,[agua]
      ,[alcantaril]
      ,[nvalec]
      ,[fnvalec]
      ,[annvalec]
      ,[lecactual]
      ,[flecactual]
      ,[anlecactua]
      ,[lecanterior]
      ,[flecanteri]
      ,[anlecanter]
      ,[lecturisac]
      ,[consumo]
      ,[pagua]
      ,[palcan]
      ,[psanea]
      ,[rezagua]
      ,[recagua]
      ,[rezalcan]
      ,[recalcan]
      ,[rezsanea]
      ,[recsanea]
      ,[recotros]
      ,[crbomb]
      ,[otros]
      ,[iva]
      ,[suma]
      ,[lecturas]
      ,[importepago]
      ,[fechapago]
      ,[vencimient]
      ,[periodo]
      ,[ppagua]
      ,[ppalcan]
      ,[ppsanea]
      ,[prezagua]
      ,[prezalcan]
      ,[prezsanea]
      ,[precagua]
      ,[precalcan]
      ,[precsanea]
      ,[precotros]
      ,[pcrbomb]
      ,[potros]
      ,[piva]
      ,[ajustes]
      ,[bonificaci]
      ,[estado]
      ,[facturacion]
      ,[estimado]
      
  FROM Historico_Simapag.[dbo].[hisrecibos]