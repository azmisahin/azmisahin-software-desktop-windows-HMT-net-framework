# ![Logo](media/favicon.png)

## HMT.Tracking - Hardware Management Tool Tracking

A management tool that provides easy access to hardware interfaces to automate management tasks on local or remote computers.

IT managers and System Specialists; provides fast and practical solutions for monitoring, reporting and auditing critical operations.

Basically, the application interface toolkit that targets windows devices plans to support other operating systems such as linux and macos in the future.

![alt text](media/HMT.Windows.Desktop.Tracking.png "Hardware Management Tool Windows Desktop Tracking")

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