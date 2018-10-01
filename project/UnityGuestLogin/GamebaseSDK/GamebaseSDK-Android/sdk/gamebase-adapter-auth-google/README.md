# Gamebase Authentication Google Adapter for Android

## Dependencies

| Module | Dependencies | Description |
| ------- | ------------ | ----------- |
| gamebase-adapter-auth-google | gamebase-sdk-base-{version}.aar | Gamebase Android SDK |
| | play-services-auth-{version}.aar | Google Account Login |
| | play-services-auth-api-phone-{version}.aar<br>play-services-auth-base-{version}.aar<br>play-services-base-{version}.aar<br>play-services-basement-{version}.aar<br>play-services-tasks-{version}.aar | play-services-auth dependencies |
| | play-services-base-{version}.aar<br>play-services-basement-{version}.aar<br>play-services-tasks-{version}.aar | play-services-auth-api-phone dependencies |
| | play-services-base-{version}.aar<br>play-services-basement-{version}.aar<br>play-services-tasks-{version}.aar | play-services-auth-base dependencies |
| | play-services-basement-{version}.aar<br>play-services-tasks-{version}.aar | play-services-base dependencies |
| | play-services-basement-{version}.aar | play-services-tasks dependencies |
| | support-v4-{version}.aar | play-services-basement dependencies |
| | support-compat-{version}.aar<br>support-media-compat-{version}.aar<br>support-core-utils-{version}.aar<br>support-core-ui-{version}.aar<br>support-fragment-{version}.aar | support-v4 dependencies |
| | support-annotations-{version}.jar<br>android.arch.lifecycle:runtime-1.0.3.aar | support-compat dependencies |
| | android.arch.lifecycle:common-1.0.3.jar<br>android.arch.core:common-1.0.0.jar<br>support-annotations-{version}.jar | runtime dependencies |
| | support-annotations-{version}.jar<br>support-compat-{version}.aar | support-media-compat dependencies |
| | support-annotations-{version}.jar<br>support-compat-{version}.aar | support-core-utils dependencies |
| | support-annotations-{version}.jar<br>support-compat-{version}.aar | support-core-ui dependencies |
| | support-compat-{version}.aar<br>support-core-ui-{version}.aar<br>support-core-utils-{version}.aar<br>support-annotations-{version}.jar | support-fragment dependencies |

### Dependencies of Google Play Services libraries
* https://developers.google.com/android/guides/setup#add_google_play_services_to_your_project

## Release Note

| Date | Google Adapter Version | Gamebase SDK Version | Google Play Service Version | Description |
| ---- | ---------------------- | -------------------- | --------------------------- | ----------- |
| 2017.03.21 | 1.1.1 | 1.1.0 ~ 1.1.2 | 10.0.1 ~ 11.8.0 | - Initial version |
| 2017.04.20 | 1.1.3 | 1.1.3 ~ 1.5.0 | 10.0.1 ~ 11.8.0 | - Change the Interface |
| 2017.09.18 | 1.2.0 | 1.1.3 ~ 1.5.0 | 10.0.1 ~ 11.8.0 | - Fix wrong null check<br> - Fix NullPointerException by forced restart. |
| 2018.02.22 | 1.7.0 | 1.7.0 ~       | 10.0.1 ~ 11.8.0 | - Change the logic that refers from the console setting value. |
| 2018.05.03 | 1.9.0 | 1.7.0 ~       | 10.0.1 ~ 11.8.0 | - Add safe logic when auth code was null. |
