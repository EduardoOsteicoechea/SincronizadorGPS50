﻿using System;
using System.Data;
using System.Data.SqlClient;

namespace SincronizadorGPS50.Workflows.Clients
{
    internal class AddClientToSyncronizationUITable
    {
        internal AddClientToSyncronizationUITable
        (
            GestprojectClient gestprojectClient,
            DataTable sincronizationTable,
            string synchronizationStatus = "Nunca ha sido sincronizado",
            string comments = ""
        )
        {
            DataRow row = sincronizationTable.NewRow();

            row[0] = synchronizationStatus;
            row[1] = gestprojectClient.synchronization_table_id;
            row[2] = gestprojectClient.PAR_ID;
            row[3] = gestprojectClient.PAR_SUBCTA_CONTABLE;
            row[4] = gestprojectClient.sage50_guid_id;
            row[5] = gestprojectClient.PAR_NOMBRE;
            row[6] = gestprojectClient.PAR_NOMBRE_COMERCIAL;
            row[7] = gestprojectClient.PAR_CIF_NIF;
            row[8] = gestprojectClient.PAR_DIRECCION_1;
            row[9] = gestprojectClient.PAR_CP_1;
            row[10] = gestprojectClient.PAR_LOCALIDAD_1;
            row[11] = gestprojectClient.PAR_PAIS_1;
            if(comments != "")
            {
                row[12] = comments;
            }
            else
            {
                row[12] = gestprojectClient.comments;
            };

            sincronizationTable.Rows.Add(row);       
        }
    }
}