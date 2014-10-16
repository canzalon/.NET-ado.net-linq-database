/*
 * Solution: .NET-ado.net-linq-database (assig4.doc)
 * Project: canzalon_a4p4
 * File/Module: VARI.cs
 * Author: Christopher Anzalone 
 * 
 */

//------------------------------------------------------------------------------
// <copyright file="CSSqlCodeFile.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Collections;
using System.Collections.Generic;

[Serializable]
[Microsoft.SqlServer.Server.SqlUserDefinedAggregate(Format.Native)]
public struct VARI
{
    public void Init()
    {
        n = 0;
        sum1 = 0;
        sum2 = 0;
    }

    public void Accumulate(SqlDouble Value)
    {
        if (!Value.IsNull)
        {
            n++;
            sum1 += (double)Value * (double)Value;
            sum2 += (double)Value;
        }
    }

    public void Merge(VARI Group)
    {
        
    }

    public SqlDouble Terminate()
    {
        return (sum1 / n) - ((sum2 / n) * (sum2 / n));
    }

    // This is a place-holder member field
    private int n;
    private double sum1;
    private double sum2;

}