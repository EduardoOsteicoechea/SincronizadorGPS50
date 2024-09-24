﻿using Infragistics.Designers.SqlEditor;
using sage.ew.cliente;
using sage.ew.db;
using SincronizadorGPS50.Sage50Connector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace SincronizadorGPS50.Sage50Connector
{
   public class CreateSage50Project
   {
      public string EntityCode { get; set; } = "";
      public string GUID_ID { get; set; } = "";
      public CreateSage50Project
      (
         string name,
         string address,
         string postalCode,
         string locality,
         string province,
         ISynchronizationTableSchemaProvider tableSchema
      )
      {
         try
         {
            int nextCodeAvailable = new GetSage50Projects(tableSchema.SageTableData).NextCodeAvailable;

            StringBuilder fieldsToQueryStringBuilder = new StringBuilder();
            foreach(var item in tableSchema.SageTableData.tableFieldsAlongTypes)
            {
               fieldsToQueryStringBuilder.Append($"{item.name},");
            };
            fieldsToQueryStringBuilder.ToString().TrimEnd(',');

            Obra entity = new Obra();

            entity._Codigo = (nextCodeAvailable++).ToString();
            entity._Nombre = name.Trim();
            entity._Direccion = address.Trim();
            entity._Codpost = postalCode.Trim();
            entity._Poblacion = locality.Trim();
            entity._Provincia = province.Trim();

            if(entity._Save())
            {
               string getSage50EntitySQLQuery = $@"
                  SELECT 
                     guid_id 
                  FROM 
                     {DB.SQLDatabase(tableSchema.SageTableData.dispatcherAndName.sageDispactcherMechanismRoute,tableSchema.SageTableData.dispatcherAndName.tableName)}
                  WHERE 
                     codigo='{entity._Codigo}'
                  ;";

               new VisualizationForm("S50 Project Creating SQL Qeury",getSage50EntitySQLQuery);

               DataTable sage50EntityDataTable = new DataTable();

               DB.SQLExec(getSage50EntitySQLQuery, ref sage50EntityDataTable);

               GUID_ID = sage50EntityDataTable.Rows[0].ItemArray[0].ToString().Trim();
            }
            else
            {
               new VisualizationForm("S50 Project Creating SQL Qeury",$@"
                   Error en la la entidad empleando estos datos:
                   EntityCode: {entity._Codigo}
                   name: {entity._Nombre}
                   address: {entity._Direccion}
                   postalCode: {entity._Codpost}
                   poblacion: {entity._Poblacion}
                   province: {entity._Provincia}
               ");
            };
            //}
            //else
            //{
            //   MessageBox.Show("Sage50 admite un máximo de 9999 clientes por grupo de empresas y su base de clientes de Gestproject supera éste límite.");
            //};
         }
         catch(System.Exception exception)
         {
            throw ApplicationLogger.ReportError(
               MethodBase.GetCurrentMethod().DeclaringType.Namespace,
               MethodBase.GetCurrentMethod().DeclaringType.Name,
               MethodBase.GetCurrentMethod().Name,
               exception
            );
         };
}
   }
}