Unity 2022.3.51f1
Rule
    • Khi cần code một tính năng gì đó, thì tạo một branch mới và làm việc trên branch đó. Tên branch sẽ đặt theo format: feature/tên-tính-năng (ví dụ thế)
        ◦ Vd: feature/open-sharingan 
    • Làm gì thì cũng làm trên branch của mình, không ai được tự ý commit sang branch chính
    • Phải yêu cầu merge request và thông báo cho admin(eddy) để duyệt request
    • Mỗi lần code thì cần phải pull từ branch chính sang branch của mình để xem có update gì không, tránh để đến lúc merge thì sẽ phải duyệt rất nhiều
    • Không động vào phần code của người khác, đối với phần code chung nếu có thay đổi gì thì cần phải thông báo
    • Không dồn nhiều thay đổi vào một lần commit. Khi commit cần ghi nội dung commit là hành động mình thực hiện
        ◦ Vd: add player movement, add sprites, update gameplay... 
Coding Conventions
    • Quy tắc đặt tên:
        ◦ Tên Class, Property, Method: Sử dụng chữ cái đầu viết hoa cho mỗi từ. Tên Method nên mô tả rõ ràng chức năng của nó 
            ▪ Vd: EnemyController, EnemyCount, SpawnEnemy() 
        ◦ Tên Field, Variable: Sử dụng chữ cái đầu viết thường, các từ sau viết hoa. Nếu access modifier là private thì thêm dấu _ trước tên 
            ▪ Vd: _enemyHealth, enemySpeed 
        ◦ Tên Constants: Sử dụng chữ cái viết hoa và thêm dấu _ giữa các từ 
            ▪ Vd: MAX_HEALTH 
        ◦ Tên Interface: Thêm I (viết hoa) vào đầu 
            ▪ Vd: IMovable 
    • Quy tắc sử dụng Unity-specific APIs:
        ◦ Awake() được gọi trước Start(), dùng để khởi tạo các biến hoặc tham chiếu giữa các đối tượng. Start() thường dùng để bắt đầu các hành động khi đối tượng đã được kích hoạt 
    • Quy tắc comment:
        ◦ Sử dụng comment để giải thích code, nhưng tránh việc comment những điều hiển nhiên 

