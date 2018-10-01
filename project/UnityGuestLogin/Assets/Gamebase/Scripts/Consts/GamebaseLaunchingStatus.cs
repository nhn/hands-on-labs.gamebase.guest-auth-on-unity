namespace Toast.Gamebase
{
    public class GamebaseLaunchingStatus
    {
        /// <summary>
        /// 정상 서비스 중
        /// </summary>
        public const int IN_SERVICE                     = 200;
        /// <summary>
        /// 업그레이드 권장
        /// </summary>
        public const int RECOMMEND_UPDATE               = 201;
        /// <summary>
        /// 점검중이지만 QA리스트로 인한 게임 접속
        /// </summary>
        public const int IN_SERVICE_BY_QA_WHITE_LIST    = 202;        
        /// <summary>
        /// 업그레이드 필수
        /// </summary>
        public const int REQUIRE_UPDATE                 = 300;
        /// <summary>
        /// 블랙리스트에 의한 접속 차단
        /// </summary>
        public const int BLOCKED_USER                   = 301;
        /// <summary>
        /// 서비스 종료됨
        /// </summary>
        public const int TERMINATED_SERVICE             = 302;
        /// <summary>
        /// 점검
        /// </summary>
        public const int INSPECTING_SERVICE             = 303;
        /// <summary>
        /// 전체 시스템 점검
        /// </summary>
        public const int INSPECTING_ALL_SERVICES        = 304;        
        /// <summary>
        /// 내부 서버 에러
        /// </summary>
        public const int INTERNAL_SERVER_ERROR          = 500;
    }
}