MainCamera = {}

function MainCamera.Awake(_gameObject)
    MainCamera.gameObject = _gameObject
    MainCamera.gameObject_Player = UnityEngine.GameObject.Find("Player")
end

function MainCamera.Start()
    MainCamera.offset = MainCamera.gameObject.transform.position - MainCamera.gameObject_Player.transform.position
end

function MainCamera.LateUpdate()
    --记录位置
    MainCamera.gameObject.transform.position = MainCamera.offset + MainCamera.gameObject_Player.transform.position
end
