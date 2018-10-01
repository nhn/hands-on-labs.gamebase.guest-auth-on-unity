# Gamebase Purchase IAP Adapter for Android

## Dependencies

| Module | Dependencies | Description |
| ------- | ------------ | ----------- |
| gamebase-adapter-purchase-iap | gamebase-sdk-base-{version}.aar | Gamebase Android SDK |
| | iap-{version}.aar | TOAST Cloud IAP Android SDK |
| | iap-onestore-{version}.aar | TOAST Cloud IAP Android SDK - ONEStore module |
| | mobill-core-{version}.aar | iap dependencies |
| | gson-2.2.4.jar<br>okhttp-1.5.4.jar | mobill-core dependancies |
| | iap-{version}.aar | iap-onestore dependencies |

## Release Note

| Date | IAP Adapter Version | Gamebase SDK Version | IAP SDK Version | Description |
| --- | --- | --- | --- | --- |
| 2017.03.09 | 1.0.0 | 1.0.0 | 1.3.2 | Initial version |
| 2017.03.21 | 1.1.0 | 1.1.0 | 1.3.2.20170119 | Update IAP for the 'ONGATE' provider |
| 2017.03.22 | 1.1.1 | 1.1.1 ~ 1.1.3 | 1.3.2.20170321 | Update IAP for the fixed empty 'currency' issue |
| 2017.04.21 | 1.1.3.1 | 1.1.3.1 | 1.3.2.20170321 | Add 'setStoreCode' API to change store code runtime |
| 2017.04.28 | 1.1.3.2 | 1.1.3.2 | 1.3.2.20170424 | Add error code for the 'not enought cash' error |
| 2017.05.23 | 1.1.4   | 1.1.4 ~ 1.1.4.1 | 1.3.2.20170424 | Update version |
| 2017.06.27 | 1.1.4.2 | 1.1.4.2 | 1.3.3.20170627 | Update IAP SDK |
| 2017.07.19 | 1.1.5   | 1.1.5 ~ 1.11.1 | 1.3.3.20170627 | Update version |
| 2017.08.08 | 1.1.5   | 1.1.5 ~ 1.11.1 | 1.3.3.5 | Update IAP SDK |
| 2017.10.26 | 1.3.0   | 1.1.5 ~ 1.11.1 | 1.3.5.1 ~ 1.3.8 | Update IAP SDK to fix issues where callback does not called when 'singleTask' launch mode |
| 2018.01.25 | 1.3.0   | 1.1.5 ~ 1.11.1 | 1.3.5.1 ~ 1.3.8 | Update IAP SDK to 1.3.7 |
| 2018.02.22 | 1.3.0   | 1.1.5 ~ 1.11.1 | 1.3.5.1 ~ 1.3.8 | Update IAP SDK to 1.3.8 |
| 2018.06.26 | 1.11.0  | 1.1.5 ~ 1.11.1 | 1.3.5.1 ~ 1.3.8 | Remove unnecessary permissions |
| 2018.08.01 | 1.12.0.1  | 1.12.0.1 ~ | 1.5.0 | Update IAP SDK to 1.5.0 <br/> Fish Island 전달용 Hotfix <br/> 정식배포에는 patch version 을 올릴 것.|
| 2018.08.09 | 1.12.1  | 1.12.0.1 ~ | 1.5.0 | Just update(synchronize) the IAP Adapter version due to release Gamebase v1.12.1 |