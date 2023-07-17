space = nil

function TestEnum(e)
    print("Enum is:" .. tostring(e))

    if space:ToInt() == 0 then
        print("enum ToInt() is ok")
    end

    if not space:Equals(0) then
        print("enum compare int is ok")
    end

    if space == e then
        print("enum compare enum is ok")
    end

    local s = UnityEngine.Space.IntToEnum(0)

    if space == s then
        print("IntToEnum change type is ok")
    end
end

function ChangeLightType(light, type)
    print("change light type to " .. tostring(type))
    light.type = type
end

function SetLightIntensity(_light, _intensity)
    _light.intensity = _intensity
end
