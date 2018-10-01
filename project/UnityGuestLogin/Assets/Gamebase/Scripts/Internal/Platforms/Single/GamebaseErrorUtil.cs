#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
using System;
using Toast.Gamebase.Single.Communicator;

namespace Toast.Gamebase.Single
{
    public class GamebaseErrorUtil
    {
        public static GamebaseError CreateGamebaseErrorByServerErrorCode(string transactionId, string apiId, CommonResponse.Header header, string domain = null)
        {
            int errorCode;

            switch (header.resultCode)
            {
                //----------------------------------------
                //  Common
                //----------------------------------------
                case GamebaseServerErrorCode.TYPE_MISS_MATCH:
                case GamebaseServerErrorCode.ILLEGAL_ARGUMENT:
                case GamebaseServerErrorCode.HTTP_MESSAGE_NOT_READABLE:
                case GamebaseServerErrorCode.MISSING_SERVLET_REQUEST_PARAMETER:
                case GamebaseServerErrorCode.METHOD_ARGUMENT_NOT_VALID:
                case GamebaseServerErrorCode.METHOD_ARGUMENT_TYPE_MISMATCH:
                case GamebaseServerErrorCode.INVALID_APP_ID:
                case GamebaseServerErrorCode.INVALID_APP_KEY:
                    errorCode = GamebaseErrorCode.INVALID_PARAMETER;
                    break;
                case GamebaseServerErrorCode.NOT_AUTHENTICATED:
                    errorCode = GamebaseErrorCode.NOT_LOGGED_IN;
                    break;
                case GamebaseServerErrorCode.UNKNOWN_SYSTEM:
                    errorCode = GamebaseErrorCode.SERVER_INTERNAL_ERROR;
                    break;
                case GamebaseServerErrorCode.REMOTE_SYSTEM:
                    errorCode = GamebaseErrorCode.SERVER_REMOTE_SYSTEM_ERROR;
                    break;
                case GamebaseServerErrorCode.BANNED_MEMBER:
                    errorCode = GamebaseErrorCode.BANNED_MEMBER;
                    break;
                case GamebaseServerErrorCode.INVALID_MEMBER:
                    errorCode = GamebaseErrorCode.INVALID_MEMBER;
                    break;
                //----------------------------------------
                //  Lighthouse
                //----------------------------------------
                case GamebaseServerErrorCode.LIGHT_HOUSE_NOT_AUTHENTICATED:
                    errorCode = GamebaseErrorCode.NOT_LOGGED_IN;
                    break;
                case GamebaseServerErrorCode.LIGHT_HOUSE_NO_SUCH_REQUEST_API:
                    errorCode = GamebaseErrorCode.NOT_SUPPORTED;
                    break;
                case GamebaseServerErrorCode.LIGHT_HOUSE_JSON_PARSING_ERROR:
                    errorCode = GamebaseErrorCode.INVALID_JSON_FORMAT;
                    break;
                case GamebaseServerErrorCode.LIGHT_HOUSE_GATEWAY_CONNECTION_ERROR:
                    errorCode = GamebaseErrorCode.SERVER_INTERNAL_ERROR;
                    break;
                //----------------------------------------
                //  Gateway
                //----------------------------------------
                case GamebaseServerErrorCode.GATEWAY_JSON_PARSING_ERROR:
                    errorCode = GamebaseErrorCode.INVALID_JSON_FORMAT;
                    break;
                case GamebaseServerErrorCode.GATEWAY_MISSING_REQUEST_PARAMETER:
                case GamebaseServerErrorCode.GATEWAY_INVALID_APP_ID:
                    errorCode = GamebaseErrorCode.INVALID_PARAMETER;
                    break;
                case GamebaseServerErrorCode.GATEWAY_INVALID_ACCESS_TOKEN:
                    errorCode = GamebaseErrorCode.AUTH_TOKEN_LOGIN_INVALID_TOKEN_INFO;
                    break;
                case GamebaseServerErrorCode.GATEWAY_PRODUCT_DATA_NOT_FOUND:
                    errorCode = GamebaseErrorCode.INVALID_PARAMETER;
                    break;
                case GamebaseServerErrorCode.GATEWAY_REQUEST_API_NOT_FOUND:
                    errorCode = GamebaseErrorCode.NOT_SUPPORTED;
                    break;
                case GamebaseServerErrorCode.GATEWAY_LOGGED_IN_IDP_COULD_NOT_DELETED:
                    errorCode = GamebaseErrorCode.AUTH_REMOVE_MAPPING_LOGGED_IN_IDP;
                    break;
                case GamebaseServerErrorCode.GATEWAY_UNKNOWN_SYSTEM:
                    errorCode = GamebaseErrorCode.SERVER_UNKNOWN_ERROR;
                    break;
                case GamebaseServerErrorCode.GATEWAY_REQUEST_WORKER_ERROR:
                case GamebaseServerErrorCode.GATEWAY_QUEUE_TIME_OUT:
                case GamebaseServerErrorCode.GATEWAY_QUEUE_CAPACITY_FULL:
                    errorCode = GamebaseErrorCode.SERVER_INTERNAL_ERROR;
                    break;
                case GamebaseServerErrorCode.GATEWAY_GB_LNC_SYSTEM_ERROR:
                    errorCode = GamebaseErrorCode.LAUNCHING_SERVER_ERROR;
                    break;
                case GamebaseServerErrorCode.GATEWAY_GB_ID_SYSTEM_ERROR:
                    errorCode = GamebaseErrorCode.SERVER_INTERNAL_ERROR;
                    break;
                //----------------------------------------
                //  Launching
                //----------------------------------------
                case GamebaseServerErrorCode.LAUNCHING_NOT_EXIST_CLIENT_ID:
                    errorCode = GamebaseErrorCode.LAUNCHING_NOT_EXIST_CLIENT_ID;
                    break;
                case GamebaseServerErrorCode.LAUNCHING_UNREGISTERED_APP:
                    errorCode = GamebaseErrorCode.LAUNCHING_UNREGISTERED_APP;
                    break;
                case GamebaseServerErrorCode.LAUNCHING_UNREGISTERED_CLIENT:
                    errorCode = GamebaseErrorCode.LAUNCHING_UNREGISTERED_CLIENT;
                    break;
                //----------------------------------------
                //  Member
                //----------------------------------------
                case GamebaseServerErrorCode.MEMBER_APP_ID_MISS_MATCH:
                    errorCode = GamebaseErrorCode.INVALID_PARAMETER;
                    break;
                case GamebaseServerErrorCode.MEMBER_USER_ID_MISS_MATCH:
                case GamebaseServerErrorCode.MEMBER_INVALID_MEMBER:
                    errorCode = GamebaseErrorCode.AUTH_INVALID_MEMBER;
                    break;
                case GamebaseServerErrorCode.MEMBER_INVALID_AUTH:
                    {
                        if (true == apiId.Equals(Lighthouse.API.Gateway.ID.TOKEN_LOGIN, StringComparison.Ordinal))
                        {
                            errorCode = GamebaseErrorCode.AUTH_TOKEN_LOGIN_INVALID_TOKEN_INFO;
                        }
                        else if (true == apiId.Equals(Lighthouse.API.Gateway.ID.IDP_LOGIN, StringComparison.Ordinal))
                        {
                            errorCode = GamebaseErrorCode.AUTH_IDP_LOGIN_FAILED;
                        }
                        else if (true == apiId.Equals(Lighthouse.API.Gateway.ID.ADD_MAPPING, StringComparison.Ordinal))
                        {
                            errorCode = GamebaseErrorCode.AUTH_ADD_MAPPING_FAILED;
                        }
                        else if (true == apiId.Equals(Lighthouse.API.Gateway.ID.REMOVE_MAPPING, StringComparison.Ordinal))
                        {
                            errorCode = GamebaseErrorCode.AUTH_REMOVE_MAPPING_FAILED;
                        }
                        else
                        {
                            errorCode = GamebaseErrorCode.AUTH_UNKNOWN_ERROR;
                        }
                        break;
                    }
                case GamebaseServerErrorCode.BANNED_MEMBER_LOGIN:
                case GamebaseServerErrorCode.AUTHKEY_BELONG_TO_BANNED_MEMBER:
                    errorCode = GamebaseErrorCode.AUTH_BANNED_MEMBER;
                    break;
                case GamebaseServerErrorCode.MEMBER_NOT_EXIST:
                    errorCode = GamebaseErrorCode.AUTH_NOT_EXIST_MEMBER;
                    break;
                case GamebaseServerErrorCode.MEMBER_LAST_MAPPED_IDP:
                    errorCode = GamebaseErrorCode.AUTH_REMOVE_MAPPING_LAST_MAPPED_IDP;
                    break;
                case GamebaseServerErrorCode.MEMBER_ALREADY_WITHDRAWN:
                    errorCode = GamebaseErrorCode.AUTH_WITHDRAW_FAILED;
                    break;
                case GamebaseServerErrorCode.MEMBER_ALREADY_MAPPED_MEMBER:
                    errorCode = GamebaseErrorCode.AUTH_ADD_MAPPING_ALREADY_MAPPED_TO_OTHER_MEMBER;
                    break;
                case GamebaseServerErrorCode.MEMBER_ALREADY_MAPPED_IDP:
                    errorCode = GamebaseErrorCode.AUTH_ADD_MAPPING_ALREADY_HAS_SAME_IDP;
                    break;
                case GamebaseServerErrorCode.CAN_NOT_ADD_GUEST_IDP:
                    errorCode = GamebaseErrorCode.AUTH_ADD_MAPPING_CANNOT_ADD_GUEST_IDP;
                    break;
                default:
                    errorCode = GamebaseErrorCode.UNKNOWN_ERROR;
                    break;
            }

            var traceError = header.traceError;

            if (true == traceError.throwPoint.Equals("gbId", StringComparison.Ordinal))
            {
                var GamebaseServerError = new GamebaseError(header.resultCode, traceError.productId, header.resultMessage);
                var colonDelimiter = new string[] { "::" };
                var colonDelimiterIndex = traceError.resultMessage.IndexOf(colonDelimiter[0]);

                if (-1 == colonDelimiterIndex)
                {
                    GamebaseServerError.error = new GamebaseError(traceError.resultCode, traceError.throwPoint, traceError.resultMessage);
                }
                else
                {
                    var gbIdMessage = traceError.resultMessage.Split(colonDelimiter, 2, StringSplitOptions.None);
                    var gbIdError = new GamebaseError(traceError.resultCode, traceError.throwPoint, gbIdMessage[0].TrimEnd());
                    GamebaseServerError.error = gbIdError;

                    var equalsSignDelimiter = new string[] { "=" };
                    var equalsSignDelimiterIndex = gbIdMessage[1].TrimStart().IndexOf(equalsSignDelimiter[0]);

                    if (-1 != equalsSignDelimiterIndex)
                    {
                        var detailMessage = gbIdMessage[1].Split(equalsSignDelimiter, 2, StringSplitOptions.None);
                        gbIdError.error = new GamebaseError(-1, detailMessage[0].TrimEnd(), detailMessage[1].TrimStart());
                    }
                }

                return new GamebaseError(errorCode, domain, string.Empty, GamebaseServerError, transactionId);
            }
            else
            {
                return new GamebaseError(errorCode, domain, string.Empty, new GamebaseError(header.resultCode, traceError.throwPoint, header.resultMessage), transactionId);
            }
        }
    }
}
#endif