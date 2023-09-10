Player = {}

function Player.Awake(_gameObject)
    print("Player.Awake")

    local transform = _gameObject.transform
    Player.gameObject = _gameObject
    Player.transform = transform
    Player.rigidbody = transform:GetComponent("Rigidbody")
end

function Player.FixedUpdate()
    --记录位置
    local lastPos = Player.currPos
    local currPos = Player.transform.position
    Player.currPos = currPos
    --计算速度
    if lastPos then
        local speed = (currPos - lastPos) / Time.fixedDeltaTime
        Debugger.Log(string.format("speed=%s", speed))
    end

    --获取输入
    local input = UnityEngine.Input
    local input_Hor, intput_Ver = input.GetAxis("Horizontal"), input.GetAxis("Vertical")

    --施加作用力
    local rigidbody = Player.rigidbody
    rigidbody:AddForce(input_Hor * 5, 0, intput_Ver * 5)
end
