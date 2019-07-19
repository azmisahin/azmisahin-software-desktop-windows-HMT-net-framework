# ![Logo](docs/media/favicon.png)

## HMT.Windows.Desktop.Tracking - Hardware Management Tool Tracking

A management tool that provides easy access to hardware interfaces to automate management tasks on local or remote computers.

IT managers and System Specialists; provides fast and practical solutions for monitoring, reporting and auditing critical operations.

Basically, the application interface toolkit that targets windows devices plans to support other operating systems such as linux and macos in the future.

## Pipeline
[![Build status](https://dev.azure.com/azmisahin-github/azmisahin-software-desktop-windows-HMT-net-framework/_apis/build/status/azmisahin-software-desktop-windows-HMT-net-framework-.NET%20Desktop-CI)](https://dev.azure.com/azmisahin-github/azmisahin-software-desktop-windows-HMT-net-framework/_build/latest?definitionId=19)

## Screen
![alt text](docs/media/HMT.Windows.Desktop.Tracking.png "Hardware Management Tool Windows Desktop Tracking")

app.config

```xml
    <appSettings>
        <!--Computer User Name-->
        <add key="User" value=""/>

        <!--Computer Password-->
        <add key="Password" value=""/>

        <!--Computer Name or Ip Address Or Local Connetection "."-->
        <add key="Computer" value=""/>

        <!--Active Directory Domain Name or Workgorup Name-->
        <add key="Domain" value=""/>

        <!--Remote Web Service-->
        <add key="api" value=""/>
    </appSettings>
```