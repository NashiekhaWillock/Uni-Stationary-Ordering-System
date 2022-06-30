Oracle.ManagedDataAccess.Core NuGet Package 2.19.70 README
==========================================================

Release Notes: Oracle Data Provider for .NET Core

April 2020


New Features
============
1. .NET Core 3.1 certification
ODP.NET Core 2.19.60 and higher is certified and supported with .NET Core 3.1.

2. Linux 8 certification
ODP.NET Core 2.19.60 and higher is certified and supported with Oracle Linux 8 and Red Hat Enterprise Linux 8.

3. Websocket and Websocket with SSL/TLS

Websocket is a protocol that offers full-duplex communication channels over a single TCP connection. WebSocket with SSL/TLS offers a secure WebSocket connection. WebSocket is an extension to HTTP and is able to work with HTTP proxies and intermediaries. ODP.NET Core now supports Websocket and Websocket with SSL/TLS.

4. Connection String attributes: Tns_Admin and Wallet_Location
ODP.NET Core can now set the TNS admin directory and wallet location directory on the connection string.

5. SYSASM Privilege

Oracle Automatic Storage Management (Oracle ASM) is a volume manager and a file system for Oracle database files. SYSASM is a system privilege that enables administrators to manage ASM instances. ODP.NET Core can now connect using the SYSASM administrative privilege to perform storage management of the Oracle Database.


Bug Fixes since Oracle.ManagedDataAccess.Core NuGet Package 2.19.60
===================================================================
Bug 30739835 : NULLREFERENCEEXCEPTION IS OBSERVED IN RED HAT KUBERNETES ON DOCKER
Bug 29849626: MAC: HANDSHAKE ERROR "ORA-00542" WHEN OPEN SSL CONNECTION AGAINST ATP/ADW

Note on Bug 29849626 fix: This bug fix now enables ODP.NET Core TLS/SSL for .NET Core 3.x. To use the feature, the System.Security.Cryptography.Pkcs (>= 4.7.0) package, available on NuGet Gallery, must be available for the application to load/use. To accomplish this, applications can add the package as a reference to the project, then rebuild or install the package to the NuGet cache, such as through the "nuget.exe install" operation."


Copyright (c) 2020, Oracle and/or its affiliates. All rights reserved.