tools = {}

function tools.add(...)
    local total = 0

    --计算
    local argArr = {...}
    for _, val in ipairs(argArr) do
        total = total + val
    end

    return total
end
