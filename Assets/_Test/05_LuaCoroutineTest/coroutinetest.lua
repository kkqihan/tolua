local function print_abc()
    local curr = string.byte("a")

    while (true) do
        --打印
        print(string.format("[print_abc] curr=%s", string.char(curr)))

        --等待1s后累加
        coroutine.step()
        if string.char(curr) == "z" then
            curr = string.byte("a")
        else
            curr = curr + 1
        end
    end
end

local function print_123()
    local curr = 0

    local haveCreateABC = false
    while (true) do
        --打印
        print(string.format("[print_123] curr=%d", curr))

        if not haveCreateABC then
            haveCreateABC = true
            
            print("coroutine.start(print_abc) Begin")
            coroutine.start(print_abc)
            print("coroutine.start(print_abc) End")
        end

        --等待1s后累加
        coroutine.step()
        curr = curr + 1
    end
end

coroutine.start(print_123)
