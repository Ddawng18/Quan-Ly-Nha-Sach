# BÁO CÁO ĐỒ ÁN: THIẾT KẾ VÀ PHÁT TRIỂN PHẦN MỀM QUẢN LÝ NHÀ SÁCH

## BookStore Management System

---

**Nhóm thực hiện:**

| Tên | MSSV |
|-----|------|
| Nguyễn Huỳnh Đăng | 31241026574 |
| Lê Công Bảo | 31241027834 |
| Trần Đại Phát | 31241026754 |
| Nguyễn Lâm Sỹ Phú | 31241020835 |
| Bành Phát Thịnh | 31241022944 |

**Giảng viên hướng dẫn:** —  
**Môn học:** Phát Triển Ứng Dụng Desktop  
**Năm học:** 2025–2026

---

## MỤC LỤC

1. [Chương I: Khảo Sát Hiện Trạng Và Xác Định Yêu Cầu](#chương-i-khảo-sát-hiện-trạng-và-xác-định-yêu-cầu)
   - 1.1 Lý do chọn đề tài
   - 1.2 Mục tiêu đề tài
   - 1.3 Phạm vi đề tài
   - 1.4 Phương pháp nghiên cứu
2. [Chương II: Khảo Sát Và Phân Tích Hệ Thống](#chương-ii-khảo-sát-và-phân-tích-hệ-thống)
   - 2.1 Phương pháp theo dõi
   - 2.2 Khảo sát hiện trạng
   - 2.3 Phân tích hiện trạng
   - 2.4 Phân tích yêu cầu hệ thống
3. [Chương III: Thiết Kế Hệ Thống](#chương-iii-thiết-kế-hệ-thống)
   - 3.1 Thiết kế Database
   - 3.2 Sơ đồ lớp
   - 3.3 Mô tả chức năng
4. [Chương IV: Thiết Kế Giao Diện](#chương-iv-thiết-kế-giao-diện)
   - 4.1 Giao diện đăng nhập
   - 4.2 Giao diện chính
5. [Chương V: Triển Khai Và Kiểm Thử](#chương-v-triển-khai-và-kiểm-thử)
   - 5.1 Công nghệ sử dụng
   - 5.2 Kiến trúc hệ thống
   - 5.3 Kiểm thử
   - 5.4 Các hạn chế đã biết
6. [Chương VI: Kết Luận Và Hướng Phát Triển](#chương-vi-kết-luận-và-hướng-phát-triển)
   - 6.1 Kết quả đạt được
   - 6.2 Hạn chế
   - 6.3 Hướng phát triển
7. [Tài Liệu Tham Khảo](#tài-liệu-tham-khảo)

---

## CHƯƠNG I: KHẢO SÁT HIỆN TRẠNG VÀ XÁC ĐỊNH YÊU CẦU

### 1.1 Lý do chọn đề tài

Trong bối cảnh nền kinh tế số đang phát triển mạnh mẽ, việc ứng dụng công nghệ thông tin vào hoạt động quản trị doanh nghiệp đã trở thành một xu thế tất yếu và là điều kiện tiên quyết để nâng cao năng lực cạnh tranh trong ngành bán lẻ hiện đại. Đối với mô hình kinh doanh nhà sách, đặc thù vận hành đòi hỏi phải kiểm soát một khối lượng danh mục hàng hóa vô cùng lớn, đa dạng về chủng loại, thể loại, tác giả và nhà xuất bản. Đồng thời, tần suất diễn ra các giao dịch nhập hàng, xuất kho, bán hàng và kiểm kê diễn ra liên tục hằng ngày. Do đó, việc thiết lập một cơ chế quản lý dữ liệu đồng bộ, chính xác và linh hoạt là yêu cầu cốt lõi đối với sự sống còn của doanh nghiệp.

Tuy nhiên, qua khảo sát thực tế tại các nhà sách quy mô vừa và nhỏ hiện nay, mô hình quản trị nội bộ vẫn còn mang tính chất truyền thống và bộc lộ nhiều hạn chế. Nhiều cơ sở vẫn đang duy trì phương pháp ghi chép sổ sách thủ công hoặc phụ thuộc nặng nề vào các công cụ bảng tính độc lập như Microsoft Excel. Phương pháp này dù có ưu điểm tạm thời là chi phí đầu tư ban đầu thấp và dễ tiếp cận, nhưng khi quy mô sản phẩm và số lượng đầu sách tăng trưởng theo thời gian, hệ thống vận hành thủ công lập tức đối mặt với những hệ lụy nghiêm trọng:

- **Sự bất đồng bộ và rủi ro sai sót dữ liệu:** Việc nhập liệu thủ công giữa các bộ phận kho và quầy thu ngân thiếu tính liên kết, dễ dẫn đến hiện tượng sai lệch thông tin, nhầm lẫn số liệu tồn kho thực tế và gây khó khăn lớn cho công tác thống kê doanh thu định kỳ.
- **Lãng phí thời gian và suy giảm hiệu suất vận hành:** Khâu xử lý bán hàng và tính toán hóa đơn bằng tay tốn rất nhiều thời gian. Đặc biệt vào các khung giờ cao điểm hoặc các dịp lễ tết khi lượng khách hàng tăng đột biến, việc thanh toán chậm trễ sẽ gây ra tình trạng ùn tắc tại quầy, tạo tâm lý mệt mỏi cho người mua và trực tiếp làm giảm trải nghiệm khách hàng.
- **Hạn chế trong việc đáp ứng nhu cầu thị trường:** Xu hướng tiêu dùng hiện đại đòi hỏi quy trình phục vụ phải nhanh chóng, chuyên nghiệp và thông tin sản phẩm phải luôn sẵn sàng để tra cứu. Việc thiếu hụt một hệ thống quản lý chuyên biệt khiến nhân viên mất nhiều thời gian tìm kiếm vị trí sách, không kịp thời giải đáp tình trạng còn hay hết hàng cho khách.

Bên cạnh đó, việc thiếu hụt một hệ thống lưu trữ dữ liệu tập trung khiến ban quản lý không thể nắm bắt được bức tranh toàn cảnh về tình hình kinh doanh theo thời gian thực, dẫn đến các quyết định nhập hàng hoặc điều chỉnh chiến lược kinh doanh mang tính cảm tính, thiếu cơ sở khoa học.

Xuất phát từ những đòi hỏi bức thiết mang tính thực tiễn nêu trên, việc phát triển một giải pháp phần mềm chuyên biệt hóa nhằm tối ưu hóa quy trình quản lý hoạt động kinh doanh nhà sách là một yêu cầu khách quan không thể trì hoãn. Đề tài **Xây dựng hệ thống quản lý nhà sách** được lựa chọn nghiên cứu nhằm giải quyết triệt để các nút thắt vận hành nội bộ, loại bỏ các thao tác thừa thãi, giảm thiểu sai sót do yếu tố con người, từ đó nâng cao hiệu quả hoạt động toàn diện và tạo dựng nền tảng vững chắc cho việc mở rộng quy mô cửa hàng trong tương lai.

### 1.2 Mục tiêu đề tài

Đề tài được thực hiện nhằm hướng tới các mục tiêu cụ thể sau:

- **Mục tiêu tổng quát:** Xây dựng phần mềm hỗ trợ quản lý các hoạt động chính của nhà sách nhằm tối ưu hóa vận hành và nâng cao hiệu quả kinh doanh.
- **Mục tiêu cụ thể:**
  - Hỗ trợ quản lý đồng bộ các hoạt động cốt lõi bao gồm: quản lý sách, kho hàng, bán hàng và thống kê doanh thu.
  - Giảm thiểu tối đa các sai sót phát sinh trong quá trình nhập liệu của nhân viên.
  - Hỗ trợ nhân viên tối ưu hóa thao tác để xử lý công việc nhanh chóng hơn.
  - Giúp người quản lý dễ dàng theo dõi sát sao và nắm bắt chính xác tình hình kinh doanh của cửa hàng.
  - Thiết lập cơ chế lưu trữ dữ liệu tập trung nhằm tạo sự thuận tiện cho việc tra cứu, quản lý về sau, đồng thời làm nền tảng để có thể mở rộng thêm các chức năng khác trong tương lai.

### 1.3 Phạm vi đề tài

Phạm vi nghiên cứu và triển khai ứng dụng của đề tài được giới hạn dựa trên các tiêu chí sau:

**Các nghiệp vụ cơ bản được thực hiện tại nhà sách:**
- Quản lý thông tin sách.
- Quản lý nhập xuất và tồn kho.
- Quản lý bán hàng và hóa đơn.
- Quản lý khách hàng.
- Thống kê doanh thu.

**Phạm vi giới hạn loại trừ:**
Để tập trung tối ưu hóa hiệu năng và chất lượng cho các tính năng cốt lõi tại cửa hàng, đề tài chưa triển khai các phân hệ mở rộng sau:
- Không tích hợp các tính năng thương mại điện tử và hoạt động bán hàng trực tuyến.
- Không bao gồm hệ thống quản lý quy trình đóng gói, vận chuyển và logistics từ xa.
- Chưa triển khai phân hệ quản lý nhập kho chuyên biệt (ImportReceipt) — hiện tại việc nhập kho được thực hiện thông qua chức năng cập nhật số lượng sách trực tiếp.
- Ứng dụng được phát triển và kiểm thử trên nền tảng **Windows** (.NET 9 Windows Forms), chưa hỗ trợ macOS hay Linux.

### 1.4 Phương pháp nghiên cứu

Để hoàn thành các mục tiêu đề ra và đảm bảo tính khoa học của sản phẩm phần mềm, đề tài áp dụng phối hợp các phương pháp nghiên cứu lý thuyết và thực nghiệm:

**1. Phương pháp khảo sát và thu thập thông tin thực tế**
- Tiến hành tiếp cận trực tiếp mô hình hoạt động tại nhà sách để quan sát thực tế luồng công việc hằng ngày của các bộ phận (kho, thu ngân, quản lý).
- Thực hiện phỏng vấn trực tiếp nhân viên vận hành và ban quản lý nhằm ghi nhận chính xác các khó khăn, bất cập của phương pháp quản lý cũ, từ đó xác định rõ ràng các yêu cầu chức năng và yêu cầu phi chức năng mà hệ thống mới cần đáp ứng.

**2. Phương pháp phân tích và thiết kế hệ thống hướng đối tượng**
- Sử dụng Ngôn ngữ mô hình hóa thống nhất (UML) để xây dựng hệ thống biểu đồ trực quan bao gồm: Biểu đồ ca sử dụng (Use Case Diagram) để xác định các tác nhân và chức năng tương ứng; Biểu đồ lớp (Class Diagram) để thiết kế cấu trúc logic của phần mềm.
- Vận dụng các nguyên tắc chuẩn hóa dữ liệu (1NF, 2NF, 3NF) để thiết kế mô hình cơ sở dữ liệu quan hệ tối ưu, đảm bảo tính toàn vẹn dữ liệu, tránh trùng lặp thông tin và tăng tốc độ truy xuất của hệ thống.

**3. Phương pháp xây dựng bản mẫu và thực nghiệm kiểm thử**
- Áp dụng quy trình phát triển phần mềm để từng bước xây dựng các phiên bản giao diện và tính năng thử nghiệm, cho phép đánh giá sớm mức độ phù hợp của phần mềm đối với thói quen thao tác của người dùng.
- Thực hiện kiểm thử hộp đen (black-box testing) trên các chức năng chính để xác nhận tính đúng đắn của luồng nghiệp vụ.

---

## CHƯƠNG II: KHẢO SÁT VÀ PHÂN TÍCH HỆ THỐNG

### 2.1 Phương pháp theo dõi

Để xây dựng hệ thống phù hợp với nhu cầu thực tế, nhóm đã tiến hành theo dõi những ứng dụng phổ biến trên thị trường, không chỉ vì số lượng sách lớn và đa dạng mà còn vì hệ thống và cách hoạt động ổn định, đơn giản và dễ tiếp cận với người dùng khách hàng.

### 2.2 Khảo sát hiện trạng

#### 2.2.1 Quan sát thực tế

Nhóm tiến hành quan sát trực tiếp hoạt động tại nhà sách nhằm tìm hiểu quy trình làm việc thực tế của nhân viên trong các công việc như nhập hàng, quản lý kho, bán hàng và thống kê doanh thu.

Thông qua quá trình quan sát, nhóm nhận thấy hầu hết các thao tác đều được thực hiện thủ công, dẫn đến mất nhiều thời gian xử lý và dễ xảy ra sai sót trong quá trình quản lý dữ liệu.

**Nhóm: Nhân viên bán hàng**

| STT | Vai trò | Mong muốn | Mục tiêu / Lợi ích |
|-----|---------|-----------|---------------------|
| 1 | Nhân viên bán hàng | Tìm kiếm sách nhanh theo tên, tác giả hoặc thể loại | Phục vụ khách hàng nhanh hơn, không để khách chờ lâu |
| 2 | Nhân viên bán hàng | Tạo đơn hàng và tính tiền tự động | Tránh nhầm lẫn khi tính tiền thủ công, tăng tốc thanh toán |
| 3 | Nhân viên bán hàng | Cập nhật trạng thái đơn hàng theo từng bước | Khách hàng biết được tình trạng đơn, giảm phát sinh khiếu nại |
| 4 | Nhân viên bán hàng | Xem lịch sử đơn hàng và in lại hóa đơn | Đối chiếu giao dịch khi có tranh chấp hoặc yêu cầu từ khách |
| 5 | Nhân viên bán hàng | Xử lý hủy đơn và tiếp nhận trả hàng | Xử lý linh hoạt các tình huống phát sinh, giữ hài lòng khách |

**Nhóm: Nhân viên kho**

| STT | Vai trò | Mong muốn | Mục tiêu / Lợi ích |
|-----|---------|-----------|---------------------|
| 1 | Nhân viên kho | Thêm sách mới vào hệ thống với đầy đủ thông tin | Đảm bảo dữ liệu sách chuẩn xác, phục vụ tìm kiếm và bán hàng |
| 2 | Nhân viên kho | Xem và cập nhật số lượng tồn kho theo thời gian thực | Chủ động nhập hàng kịp thời, tránh hết hàng đột ngột |
| 3 | Nhân viên kho | Nhận cảnh báo khi sách xuống dưới mức tối thiểu | Không bỏ lỡ cơ hội bán vì sách hết mà không hay |
| 4 | Nhân viên kho | Lập đơn nhập hàng từ nhà cung cấp | Quản lý nhập hàng có hệ thống, dễ đối chiếu về sau |
| 5 | Nhân viên kho | Tra cứu lịch sử nhập hàng theo nhà cung cấp | Kiểm tra tần suất và giá trị giao dịch để đàm phán tốt hơn |

**Nhóm: Quản lý / Chủ cửa hàng**

| STT | Vai trò | Mong muốn | Mục tiêu / Lợi ích |
|-----|---------|-----------|---------------------|
| 1 | Quản lý | Xem báo cáo doanh thu theo ngày, tháng, năm | Nắm bắt tình hình kinh doanh, phát hiện xu hướng sớm |
| 2 | Quản lý | Xem danh sách sách bán chạy nhất trong kỳ | Ra quyết định nhập thêm hay giảm tồn dựa trên dữ liệu thực |
| 3 | Quản lý | Xuất báo cáo ra file CSV, Excel hoặc văn bản | Lưu trữ, chia sẻ với kế toán hoặc đối tác không cần vào hệ thống |
| 4 | Quản lý | Quản lý danh sách nhà cung cấp và đơn nhập hàng | Kiểm soát chi phí đầu vào và duy trì quan hệ với đối tác |
| 5 | Quản lý | Phân quyền tài khoản cho từng nhân viên | Bảo mật dữ liệu, tránh truy cập không được phép |

#### 2.2.2 Phỏng vấn người dùng

Để có cái nhìn sâu sắc và toàn diện hơn bên cạnh việc quan sát thực tế, nhóm nghiên cứu đã tiến hành phương pháp phỏng vấn trực tiếp (Semi-structured Interview) với các đối tượng cốt lõi tham gia vào vận hành nhà sách, bao gồm: Chủ cửa hàng (Quản lý), Nhân viên bán hàng và Nhân viên kho. Quá trình này giúp làm rõ những khó khăn ẩn sâu trong quy trình thủ công mà việc quan sát chưa thể hiện hết.

**a) Nội dung và kết quả phỏng vấn các nhóm đối tượng**

- **Đối với Quản lý / Chủ cửa hàng:** Nội dung câu hỏi tập trung vào cách thức tổng hợp doanh thu, quản lý nhà cung cấp, kiểm soát chi phí đầu vào và mong muốn bảo mật thông tin nội bộ.
  - Kết quả: Người quản lý chia sẻ rằng việc phải đối chiếu thủ công nhiều file dữ liệu Excel khác nhau vào cuối tháng cực kỳ tốn thời gian và dễ xảy ra sai sót. Họ có nhu cầu cấp thiết về một công cụ có thể tự động tổng hợp số liệu, xuất báo cáo trực quan dưới dạng Excel/CSV để làm việc với đối tác và cần cơ chế phân quyền rõ ràng để nhân viên không can thiệp vào dữ liệu tài chính.

- **Đối với Nhân viên bán hàng:**
  - Nội dung câu hỏi: Tập trung vào quy trình đón tiếp khách, tra cứu đầu sách và thanh toán hóa đơn trong các khung giờ cao điểm.
  - Kết quả: Nhân viên bán hàng phản ánh áp lực lớn nhất là khi cửa hàng đông khách, việc tính tiền thủ công bằng máy tính cầm tay hoặc ghi hóa đơn giấy rất dễ nhầm lẫn. Họ mong muốn hệ thống có thanh tìm kiếm thông minh theo tên/tác giả và tích hợp thanh toán mã QR tự động tính tiền để đẩy nhanh tốc độ phục vụ.

- **Đối với Nhân viên kho:**
  - Nội dung câu hỏi: Tập trung vào quy trình nhập hàng, kiểm kê định kỳ và cách xử lý khi các đầu sách sắp hết.
  - Kết quả: Nhân viên kho cho biết họ không thể nắm bắt chính xác lượng tồn kho theo thời gian thực. Nhiều trường hợp sách đã hết hẳn trên kệ nhưng không biết để nhập thêm, hoặc nhập trùng lặp đầu sách do thông tin lưu trữ không đồng bộ. Họ yêu cầu hệ thống phải có tính năng tự động cảnh báo khi số lượng tồn kho xuống dưới mức tối thiểu.

**b) Tổng hợp nhu cầu cốt lõi (User Requirements)**

Qua phỏng vấn, nhóm đã chuẩn hóa các mong muốn thành danh sách nhu cầu người dùng cụ thể (US-01 đến US-15), làm cơ sở vững chắc cho việc thiết kế các chức năng hệ thống ở Chương 3.

#### 2.2.3 Thu thập và phân tích dữ liệu

Song song với việc quan sát và phỏng vấn, nhóm đã tiến hành thu thập các tài liệu, biểu mẫu, sổ sách thực tế đang được sử dụng tại nhà sách để phân tích cấu trúc dữ liệu nền tảng.

**a) Nguồn dữ liệu thu thập**
- Hồ sơ Sách và Danh mục: Các file Excel theo dõi danh sách đầu sách, bao gồm các trường thông tin cơ bản: Tên sách, Tác giả, Nhà xuất bản, Năm xuất bản, Số lượng tồn kho và Giá tiền.
- Chứng từ Kho hàng: Các biên bản giao nhận hàng, hóa đơn nhập kho từ các nhà cung cấp.
- Chứng từ Bán hàng: Các hóa đơn bán lẻ trao cho khách hàng (chủ yếu là hóa đơn giấy viết tay hoặc biểu mẫu in thô sơ).
- Thông tin Khách hàng và Đối tác: Danh sách số điện thoại khách hàng thân thiết ghi trong sổ và thông tin liên hệ của các nhà cung cấp.

**b) Phân tích và chuyển hóa dữ liệu (Data Analysis)**

Sau khi thu thập, nhóm tiến hành bóc tách cấu trúc dữ liệu thủ công nhằm chuẩn hóa và chuyển đổi thành mô hình dữ liệu quan hệ (Relational Database) phù hợp cho phần mềm mới:

1. **Chuẩn hóa thực thể Sách (Book):** Nhóm nhận thấy cách quản lý cũ thiếu mã định danh chuẩn quốc tế, dẫn đến việc trùng lặp dữ liệu. Vì vậy, thực thể Book trong hệ thống mới bắt buộc phải tích hợp mã ISBN (VARCHAR) để làm khóa quản lý, đồng thời liên kết chặt chẽ với mã thể loại CategoryID và mã nhà cung cấp SupplierID.

2. **Xây dựng cấu trúc Đơn hàng (Order & OrderDetail):** Từ các hóa đơn bán lẻ thô sơ, dữ liệu được bóc tách thành 2 tầng logic: Order (lưu thông tin chung: ID đơn, ngày tạo, tổng tiền, trạng thái thanh toán, nhân viên thực hiện, khách hàng mua) và OrderDetail (lưu chi tiết từng cuốn sách, số lượng mua, đơn giá và thành tiền tương ứng). Cách tổ chức này đảm bảo dữ liệu không bị phân rã và hỗ trợ truy xuất lịch sử chính xác.

3. **Quản lý đối tượng con người (Account, Employee, Customer):** Phân tách rõ ràng giữa thông tin hành chính nhân viên (Employee), tài khoản đăng nhập hệ thống (Account với trường Role, Password) và thông tin khách hàng (Customer) để phục vụ các chức năng phân quyền cũng như lưu lịch sử mua sắm.

Kết luận: Kết quả phân tích dữ liệu từ mục này đã trực tiếp hình thành nên cấu trúc sơ đồ thực thể mối quan hệ (ERD) và thiết kế Database chi tiết của hệ thống, đảm bảo tính toàn vẹn dữ liệu, loại bỏ hoàn toàn các hạn chế của việc lưu trữ bằng sổ sách hay Excel cũ.

### 2.3 Phân tích hiện trạng

Qua quá trình khảo sát thực tế, nhóm nhận thấy hệ thống quản lý hiện tại của nhà sách còn tồn tại nhiều hạn chế do phụ thuộc chủ yếu vào phương pháp quản lý thủ công.

#### 2.3.1 Quản lý sách

Thông tin sách hiện đang được lưu bằng Excel hoặc sổ ghi chép. Khi nhập sách mới, nhân viên phải tự cập nhật các thông tin như tên sách, tác giả, nhà xuất bản, giá bán và số lượng tồn kho.

**Hạn chế:**
- Dữ liệu dễ bị trùng lặp hoặc sai sót.
- Khó tìm kiếm khi số lượng đầu sách lớn.
- Mất nhiều thời gian cập nhật dữ liệu.
- Dễ xảy ra chênh lệch giữa dữ liệu thực tế và dữ liệu lưu trữ.

#### 2.3.2 Quản lý kho hàng

Việc nhập kho và kiểm kê kho vẫn được thực hiện thủ công. Nhân viên phải trực tiếp kiểm tra số lượng sách thực tế rồi đối chiếu với dữ liệu lưu trong file quản lý.

**Hạn chế:**
- Kiểm kê mất nhiều thời gian.
- Không theo dõi được tồn kho theo thời gian thực.
- Khó phát hiện các đầu sách sắp hết hàng.
- Dễ xảy ra thất thoát hàng hóa.

#### 2.3.3 Quản lý bán hàng

Trong quá trình bán hàng, nhân viên phải tự tìm kiếm sản phẩm, tính tổng tiền và ghi hóa đơn thủ công.

**Hạn chế:**
- Tính tiền dễ xảy ra nhầm lẫn.
- Thời gian thanh toán chậm khi lượng khách đông.
- Dễ thất lạc hóa đơn.
- Khó kiểm tra lịch sử giao dịch.

#### 2.3.4 Thống kê và báo cáo

Việc tổng hợp doanh thu được thực hiện bằng cách đối chiếu nhiều file dữ liệu khác nhau.

**Hạn chế:**
- Tốn nhiều thời gian tổng hợp.
- Dữ liệu dễ sai lệch.
- Khó theo dõi sách bán chạy.
- Khó đánh giá hiệu quả kinh doanh.

Từ những hạn chế trên, có thể thấy việc xây dựng hệ thống quản lý nhà sách là cần thiết nhằm nâng cao hiệu quả quản lý và tối ưu hóa hoạt động kinh doanh.

### 2.4 Phân tích yêu cầu hệ thống

#### 2.4.1 Yêu cầu chức năng

Từ kết quả khảo sát thực tế, nhóm xác định phần mềm cần đáp ứng một số yêu cầu chính như sau:

- **Quản lý sách:** Cho phép thêm, sửa và xóa thông tin sách. Mỗi đầu sách cần được lưu các thông tin cơ bản như mã ISBN, tên sách, tác giả, thể loại, giá bán và số lượng tồn kho.
- **Quản lý kho:** Theo dõi số lượng sách nhập vào và bán ra nhằm cập nhật tồn kho tự động. Hệ thống cần hỗ trợ kiểm tra số lượng còn lại của từng sản phẩm để tránh tình trạng thiếu hàng.
- **Quản lý bán hàng:** Hỗ trợ tạo hóa đơn, tính tổng tiền tự động và lưu lại lịch sử giao dịch. Việc thanh toán cần được thực hiện nhanh chóng để giảm thời gian chờ của khách hàng.
- **Quản lý khách hàng:** Lưu trữ thông tin khách hàng nhằm thuận tiện cho việc tra cứu lịch sử mua hàng và hỗ trợ các chương trình chăm sóc khách hàng sau này.
- **Báo cáo và thống kê:** Hỗ trợ thống kê doanh thu theo từng khoảng thời gian và theo dõi số lượng sách bán ra để người quản lý dễ dàng đánh giá tình hình kinh doanh.
- **Quản lý tài khoản:** Cho phép đăng nhập và phân quyền người dùng nhằm đảm bảo an toàn dữ liệu trong quá trình sử dụng.
- **Quản lý thanh toán:** Tích hợp QR code của các ngân hàng hay ví điện tử như MoMo để thuận tiện cho việc thanh toán.

#### 2.4.2 Yêu cầu phi chức năng

Ngoài các chức năng chính, hệ thống cần đáp ứng các yêu cầu phi chức năng sau:

**a) Hiệu năng**
- Hệ thống phải xử lý nhanh và ổn định khi có nhiều người sử dụng cùng lúc.
- Thời gian tìm kiếm và truy xuất dữ liệu phải ngắn.

**b) Bảo mật**
- Đảm bảo an toàn dữ liệu người dùng và dữ liệu giao dịch.
- Hỗ trợ phân quyền truy cập (Admin/Staff).
- Mật khẩu được lưu trữ trong cơ sở dữ liệu (có thể nâng cấp lên băm mật khẩu trong phiên bản sau).

**c) Giao diện**
- Giao diện thân thiện, dễ sử dụng.
- Bố cục rõ ràng, dễ thao tác đối với nhân viên và khách hàng.

**d) Khả năng mở rộng**
- Hệ thống có thể mở rộng thêm các chức năng mới trong tương lai như bán hàng online hoặc ứng dụng di động.
- Dễ nâng cấp và bảo trì.

**e) Tính ổn định**
- Hệ thống hoạt động liên tục và hạn chế lỗi phát sinh.

**f) Tính tương thích**
- Hệ thống hoạt động trên nền tảng Windows 10/11.
- Hỗ trợ kết nối với cơ sở dữ liệu SQL Server và các dịch vụ thanh toán điện tử.

#### 2.4.3 Yêu cầu người dùng

- Đối với **nhân viên bán hàng**, phần mềm cần hỗ trợ tìm kiếm sách nhanh và tạo hóa đơn dễ dàng để phục vụ khách hàng hiệu quả hơn.
- Đối với **nhân viên kho**, chương trình cần hỗ trợ theo dõi số lượng nhập xuất và kiểm tra tồn kho chính xác.
- Đối với **người quản lý**, hệ thống cần cung cấp chức năng thống kê doanh thu và hỗ trợ quản lý hoạt động của cửa hàng một cách thuận tiện.

---

## CHƯƠNG III: THIẾT KẾ HỆ THỐNG

### 3.1 Thiết kế Database

Cơ sở dữ liệu của hệ thống được triển khai trên nền tảng Microsoft SQL Server, với **8 bảng** được thiết kế logic, đảm bảo mối liên kết chặt chẽ giữa các đối tượng dữ liệu. Các bảng chính bao gồm:

| # | Bảng | Mô tả |
|---|------|-------|
| 1 | **Categories** | Danh mục/thể loại sách |
| 2 | **Suppliers** | Nhà cung cấp sách |
| 3 | **Books** | Thông tin sách (có liên kết Category, Supplier) |
| 4 | **Customers** | Thông tin khách hàng và điểm tích lũy |
| 5 | **Employees** | Thông tin nhân viên |
| 6 | **Accounts** | Tài khoản đăng nhập (liên kết Employee) |
| 7 | **Orders** | Đơn hàng (liên kết Customer, Employee) |
| 8 | **OrderDetails** | Chi tiết đơn hàng (liên kết Order, Book) |

Cơ sở dữ liệu đảm bảo tính toàn vẹn thông qua hệ thống khóa chính (PRIMARY KEY), khóa ngoại (FOREIGN KEY REFERENCES), ràng buộc UNIQUE trên ISBN và các ràng buộc nghiệp vụ cụ thể (DEFAULT, NOT NULL). Thiết kế tuân thủ chuẩn hóa 3NF: không có phụ thuộc bắc cầu, mọi thuộc tính không khóa đều phụ thuộc trực tiếp vào khóa chính.

**Chi tiết các bảng:**

**Categories** — Danh mục sách
| Cột | Kiểu dữ liệu | Ràng buộc |
|-----|-------------|-----------|
| CategoryID | INT IDENTITY(1,1) | PRIMARY KEY |
| CategoryName | NVARCHAR(100) | NOT NULL |

**Suppliers** — Nhà cung cấp
| Cột | Kiểu dữ liệu | Ràng buộc |
|-----|-------------|-----------|
| SupplierID | INT IDENTITY(1,1) | PRIMARY KEY |
| SupplierName | NVARCHAR(150) | NOT NULL |
| Address | NVARCHAR(250) | |
| Email | NVARCHAR(100) | |
| Phone | NVARCHAR(20) | |

**Books** — Sách
| Cột | Kiểu dữ liệu | Ràng buộc |
|-----|-------------|-----------|
| BookID | INT IDENTITY(1,1) | PRIMARY KEY |
| CategoryID | INT | FOREIGN KEY → Categories |
| SupplierID | INT | FOREIGN KEY → Suppliers |
| Title | NVARCHAR(200) | NOT NULL |
| Author | NVARCHAR(150) | NOT NULL |
| ISBN | NVARCHAR(20) | NOT NULL, UNIQUE |
| Publisher | NVARCHAR(150) | |
| PublishYear | INT | |
| ImportPrice | DECIMAL(18,2) | NOT NULL, DEFAULT 0 |
| SellPrice | DECIMAL(18,2) | NOT NULL, DEFAULT 0 |
| QuantityInStock | INT | NOT NULL, DEFAULT 0 |
| LastImportDate | DATETIME | |
| LastSoldDate | DATETIME | |
| IsDeleted | BIT | NOT NULL, DEFAULT 0 (xóa mềm) |

**Customers** — Khách hàng
| Cột | Kiểu dữ liệu | Ràng buộc |
|-----|-------------|-----------|
| CustomerID | INT IDENTITY(1,1) | PRIMARY KEY |
| FullName | NVARCHAR(150) | NOT NULL |
| Phone | NVARCHAR(20) | |
| Address | NVARCHAR(250) | |
| LoyaltyPoints | INT | DEFAULT 0 |
| CreatedDate | DATETIME | DEFAULT GETDATE() |

**Employees** — Nhân viên
| Cột | Kiểu dữ liệu | Ràng buộc |
|-----|-------------|-----------|
| EmployeeID | INT IDENTITY(1,1) | PRIMARY KEY |
| FullName | NVARCHAR(150) | NOT NULL |
| Phone | NVARCHAR(20) | |
| Salary | DECIMAL(18,2) | DEFAULT 0 |
| Position | NVARCHAR(100) | |
| Role | NVARCHAR(50) | DEFAULT 'Staff' |
| CreatedDate | DATETIME | DEFAULT GETDATE() |

**Accounts** — Tài khoản đăng nhập
| Cột | Kiểu dữ liệu | Ràng buộc |
|-----|-------------|-----------|
| AccountID | INT IDENTITY(1,1) | PRIMARY KEY |
| EmployeeID | INT | FOREIGN KEY → Employees |
| Username | NVARCHAR(50) | NOT NULL, UNIQUE |
| Password | NVARCHAR(256) | NOT NULL |
| Role | NVARCHAR(50) | DEFAULT 'Staff' |
| FullName | NVARCHAR(150) | NOT NULL |
| IsActive | BIT | DEFAULT 1 |

> **Ghi chú về bảo mật:** Hiện tại, mật khẩu được lưu dưới dạng plaintext trong cột `Password`. Trong phiên bản tiếp theo, nhóm dự kiến nâng cấp lên băm mật khẩu sử dụng BCrypt hoặc SHA-256 để tăng cường bảo mật.

**Orders** — Đơn hàng
| Cột | Kiểu dữ liệu | Ràng buộc |
|-----|-------------|-----------|
| OrderID | INT IDENTITY(1,1) | PRIMARY KEY |
| CustomerID | INT | FOREIGN KEY → Customers |
| EmployeeID | INT | FOREIGN KEY → Employees |
| OrderDate | DATETIME | DEFAULT GETDATE() |
| SubtotalAmount | DECIMAL(18,2) | DEFAULT 0 |
| DiscountAmount | DECIMAL(18,2) | DEFAULT 0 |
| TaxAmount | DECIMAL(18,2) | DEFAULT 0 |
| TotalAmount | DECIMAL(18,2) | DEFAULT 0 |
| PaymentStatus | NVARCHAR(20) | DEFAULT 'Pending' |
| PaymentMethod | NVARCHAR(50) | |
| PaymentTransactionId | NVARCHAR(100) | |
| LoyaltyPointsEarned | INT | DEFAULT 0 |

**OrderDetails** — Chi tiết đơn hàng
| Cột | Kiểu dữ liệu | Ràng buộc |
|-----|-------------|-----------|
| OrderDetailID | INT IDENTITY(1,1) | PRIMARY KEY |
| OrderID | INT | FOREIGN KEY → Orders |
| BookID | INT | FOREIGN KEY → Books |
| Quantity | INT | NOT NULL |
| UnitPrice | DECIMAL(18,2) | NOT NULL |
| DiscountAmount | DECIMAL(18,2) | DEFAULT 0 |
| Subtotal | DECIMAL(18,2) | NOT NULL |

**Sơ đồ quan hệ (ERD):** Xem tệp [`docs/ERD.png`](docs/ERD.png).

Việc giao tiếp với cơ sở dữ liệu được thực hiện thông qua lớp trung gian [`DbConnectionFactory.cs`](DAL/DbConnectionFactory.cs) (tầng DAL), giúp tách biệt rõ ràng giữa giao diện người dùng và tầng lưu trữ dữ liệu, đồng thời tăng tính linh hoạt và khả năng bảo trì của hệ thống. Kết nối sử dụng ADO.NET thông qua thư viện `Microsoft.Data.SqlClient`.

**Dữ liệu mẫu:** Tập lệnh [`database.sql`](database.sql) tự động tạo cơ sở dữ liệu `BookStoreDb`, 8 bảng, và chèn dữ liệu mẫu: 20 đầu sách, 4 danh mục, 3 nhà cung cấp, 3 khách hàng, 3 nhân viên, 2 tài khoản đăng nhập (admin/1 và E/2), 3 đơn hàng mẫu với 5 chi tiết đơn hàng.

### 3.2 Sơ đồ lớp

Hệ thống được thiết kế theo kiến trúc phân lớp (Layered Architecture) với 4 tầng chính:

```
┌─────────────────────────────────────────────────┐
│  Tầng UI (Windows Forms)                        │
│  Forms: LoginForm, MainForm, PosForm, ...       │
│  UserControls: BookControl, OrdersControl, ...  │
│  Theme: AppTheme, AppBranding                   │
│  ServiceLocator (DI thủ công)                   │
├─────────────────────────────────────────────────┤
│  Tầng BLL (Business Logic Layer)                │
│  Services: BookService, OrderService, ...       │
│  Payments: MomoPaymentProvider, VNPay...        │
│  Validators, LoyaltyService, FileLogger         │
├─────────────────────────────────────────────────┤
│  Tầng DAL (Data Access Layer)                   │
│  Repositories: BookRepository, OrderRepo...     │
│  DbConnectionFactory (ADO.NET)                  │
├─────────────────────────────────────────────────┤
│  Tầng DTO (Data Transfer Objects)               │
│  Models: Book, Customer, Order, ...             │
│  Enums: OrderStatus, DiscountType, ...          │
│  ValidationResult, ReportSectionDto             │
├─────────────────────────────────────────────────┤
│  Utilities (Tiện ích)                           │
│  FileLogger, ReportExporter                     │
└─────────────────────────────────────────────────┘
```

**Các lớp chính:**

- **Tầng DTO:** `Book`, `Customer`, `Employee`, `Order`, `OrderDetail`, `Category`, `Supplier`, `Account`, `CartLine`, `CheckoutRequest`, `CheckoutResult`, `ValidationResult`, `ReportSectionDto`, `DashboardMetricDto`, `PaymentConfig`, `PaymentStatus`, v.v.
- **Tầng DAL:** `DbConnectionFactory`, các interface `IRepository` và implementation `Repository` (Book, Order, Customer, Employee, Account, Category, Supplier, Dashboard, Report).
- **Tầng BLL:** Các interface `IService` và implementation `Service` (Book, Order, Customer, Employee, Auth, POS, Report, Dashboard, Loyalty). Hệ thống thanh toán gồm `IPaymentProvider`, `DemoPaymentProvider`, `MomoPaymentProvider`, `VNPayPaymentProvider`, `PaymentProviderFactory`.
- **Tầng UI:** `ServiceLocator` (DI thủ công), các Form (`PosForm`, `OrderCreateForm`, `BookEditForm`, `MainForm`, `LoginForm`, `PaymentQRForm`) và UserControls (`BookControl`, `OrdersControl`, `ReportsControl`, `DashboardControl`, `CustomersControl`, `EmployeesControl`, `CategoryControl`, `SupplierControl`).
- **Utilities:** `FileLogger` (ghi log theo ngày), `ReportExporter` (xuất CSV, HTML-Excel, văn bản).

**Sơ đồ lớp (Class Diagram):** Xem tệp [`docs/class-diagram.png`](docs/class-diagram.png).

**Dependency Injection:** Hệ thống sử dụng mẫu Service Locator thủ công thông qua lớp tĩnh [`ServiceLocator.cs`](BookStoreApp/ServiceLocator.cs). Tất cả service và repository được khởi tạo một lần dưới dạng singleton trong constructor tĩnh. Các UI control lấy service từ đây thay vì tự khởi tạo. Mẫu này phù hợp với quy mô dự án hiện tại; trong tương lai có thể nâng cấp lên container DI chính thống (`Microsoft.Extensions.DependencyInjection`).

### 3.3 Mô tả chức năng

#### 3.3.1 Quản lý sách

Chức năng bao gồm các nghiệp vụ thêm, sửa, xóa và tìm kiếm sách theo mã sách, tên sách hoặc các tiêu chí liên quan như tác giả, nhà xuất bản và thể loại. Ngoài ra, hệ thống còn hỗ trợ quản lý phân loại sách (Categories), nhà cung cấp (Suppliers), với mỗi phần đều cho phép thực hiện các thao tác CRUD tương tự.

| Thành phần | Nội dung mô tả |
|------------|----------------|
| Use case name | Quản lý thông tin sách |
| Scenario | Thêm mới / cập nhật / xóa sách trong hệ thống |
| Triggering event | Nhân viên muốn cập nhật đầu sách mới vào kho dữ liệu |
| Brief description | Mô tả cách thức thực hiện các thao tác quản lý thông tin sách trong nhà sách, bao gồm thêm, sửa, xóa và tìm kiếm sách, thể loại, nhà cung cấp và tác giả. |
| Actors | Admin/Staff |
| Preconditions | Nhân viên đã đăng nhập; Danh mục (Thể loại, NXB) đã tồn tại |
| Postconditions | Thông tin về sách, thể loại, nhà cung cấp hoặc tác giả được thêm, cập nhật hoặc xóa thành công |
| Flow of activities | 1. Người dùng chọn chức năng "Quản lý sách" từ menu hệ thống. 2. Hệ thống hiển thị danh sách sách hiện có. 3. Người dùng có thể thực hiện một trong các hành động sau: Thêm mới, Sửa, Xóa, Tìm kiếm. 4. Hệ thống thực hiện lưu trữ, xác nhận và hiển thị thông báo kết quả. |
| Exception conditions | ISBN đã tồn tại (ràng buộc UNIQUE trong DB). Thiếu trường bắt buộc (tên sách, giá). |

> **Triển khai:** [`BookService.cs`](BLL/BookService.cs), [`BookRepository.cs`](DAL/Repositories/BookRepository.cs), [`BookControl.cs`](BookStoreApp/UserControls/BookControl.cs), [`BookEditForm.cs`](BookStoreApp/Forms/BookEditForm.cs). Sách sử dụng cơ chế xóa mềm (IsDeleted = 1) thay vì xóa cứng.

#### 3.3.2 Bán hàng (POS)

Bao gồm tạo giỏ hàng, áp dụng giảm giá từng dòng và giảm giá đơn hàng (theo phần trăm hoặc số tiền cố định), tính thuế, đổi điểm tích lũy và thanh toán đa dạng (tiền mặt / QR). Hệ thống tự động kiểm tra tồn kho trước khi cho phép thêm vào giỏ hàng.

| Thành phần | Nội dung mô tả |
|------------|----------------|
| Use case name | Bán hàng (POS) |
| Scenario | Khách hàng đặt sách, nhân viên xử lý và thanh toán |
| Triggering event | Khi khách hàng yêu cầu mua sách tại quầy |
| Brief description | Mô tả quy trình bán hàng tại quầy (Point of Sale), bao gồm tạo giỏ hàng, áp dụng giảm giá, tính thuế, đổi điểm tích lũy và thanh toán. |
| Actors | Admin, Staff |
| Preconditions | Nhân viên đã đăng nhập vào hệ thống. Khách hàng có tài khoản hợp lệ trong hệ thống (hoặc khách lẻ). Sách có sẵn trong kho và còn hàng. |
| Postconditions | Thông tin đơn hàng được cập nhật chính xác trong cơ sở dữ liệu. Tồn kho sách được cập nhật (trừ số lượng). Điểm tích lũy khách hàng được cập nhật. |
| Flow of activities | 1. Nhân viên chọn chức năng "Bán hàng POS" từ menu hệ thống. 2. Hệ thống hiển thị giao diện POS với danh sách sách và khách hàng. 3. Nhân viên chọn khách hàng, thêm sách vào giỏ hàng với số lượng, loại giảm giá dòng. 4. Hệ thống tính tổng tiền tự động (tạm tính, giảm giá, thuế, đổi điểm). 5. Nhân viên chọn phương thức thanh toán (Tiền mặt hoặc QR) và hoàn tất. 6. Hệ thống lưu đơn hàng, cập nhật tồn kho và điểm tích lũy. |
| Exception conditions | Mất kết nối mạng khi lưu đơn hàng: Hệ thống hiển thị thông báo lỗi. Lỗi cơ sở dữ liệu khi tạo đơn hàng: Transaction được rollback, hệ thống hiển thị thông báo lỗi. |

> **Triển khai:** [`PosService.cs`](BLL/PosService.cs), [`PosForm.cs`](BookStoreApp/Forms/PosForm.cs), [`PaymentQRForm.cs`](BookStoreApp/Forms/PaymentQRForm.cs). Tạo đơn hàng sử dụng transaction tại [`OrderRepository.cs:54-111`](DAL/Repositories/OrderRepository.cs:54) với rollback khi lỗi. Thanh toán QR sử dụng pattern Strategy qua `IPaymentProvider` với 3 trình cung cấp: MoMo, VNPay, và Demo.

#### 3.3.3 Quản lý khách hàng

Tập trung vào việc lưu trữ thông tin khách hàng, tra cứu nhanh theo tên hoặc số điện thoại, đồng thời hỗ trợ cập nhật, thêm mới hoặc xóa thông tin khách hàng. Hệ thống tích hợp chương trình tích điểm thưởng (Loyalty Points) để khuyến khích khách hàng quay lại mua sắm.

| Thành phần | Nội dung mô tả |
|------------|----------------|
| Use case name | Quản lý khách hàng |
| Scenario | Quản lý xem danh sách khách hàng |
| Triggering event | Khi người dùng chọn chức năng quản lý khách hàng. |
| Brief description | Mô tả cách quản lý thông tin khách hàng trong hệ thống, bao gồm thêm mới, chỉnh sửa, xóa và tra cứu khách hàng |
| Actors | Admin |
| Preconditions | Người dùng đã đăng nhập. Hệ thống đang hoạt động bình thường. |
| Postconditions | Thông tin khách hàng được thêm, cập nhật hoặc xóa thành công trong cơ sở dữ liệu. |
| Flow of activities | 1. Người dùng chọn chức năng "Quản lý khách hàng" từ menu hệ thống. 2. Hệ thống hiển thị danh sách khách hàng hiện có. 3. Người dùng có thể thêm khách hàng mới, chỉnh sửa thông tin khách hàng, xóa khách hàng hoặc tra cứu khách hàng. 4. Hệ thống xác nhận và thông báo kết quả. |
| Exception conditions | Lỗi cơ sở dữ liệu khi thêm hoặc sửa thông tin: Hệ thống hiển thị thông báo lỗi. |

> **Triển khai:** [`CustomerService.cs`](BLL/CustomerService.cs), [`CustomerRepository.cs`](DAL/Repositories/CustomerRepository.cs), [`CustomersControl.cs`](BookStoreApp/UserControls/CustomersControl.cs). Hỗ trợ xem lịch sử mua hàng và điểm tích lũy của từng khách hàng. Chương trình tích điểm: [`LoyaltyService.cs`](BLL/LoyaltyService.cs).

#### 3.3.4 Quản lý đơn hàng

Cho phép xem danh sách đơn hàng, chi tiết từng đơn, lọc theo khoảng thời gian và trạng thái thanh toán. Nhân viên có thể cập nhật trạng thái đơn hàng theo quy tắc nghiệp vụ.

| Thành phần | Nội dung mô tả |
|------------|----------------|
| Use case name | Quản lý đơn hàng |
| Scenario | Cập nhật trạng thái đơn hàng trong hệ thống |
| Triggering event | Người dùng chọn chức năng "Đơn hàng" trên hệ thống. |
| Brief description | Mô tả cách quản lý danh sách đơn hàng đã tạo, bao gồm xem chi tiết, lọc theo thời gian/trạng thái và cập nhật trạng thái thanh toán. |
| Actors | Admin, Staff |
| Preconditions | Người dùng đã đăng nhập. Hệ thống đang hoạt động bình thường. |
| Postconditions | Trạng thái đơn hàng được cập nhật chính xác. Danh sách đơn hàng hiển thị đúng theo bộ lọc. |
| Flow of activities | 1. Người dùng chọn chức năng "Quản lý đơn hàng". 2. Hệ thống hiển thị danh sách đơn hàng. 3. Người dùng có thể lọc theo ngày, trạng thái, tìm kiếm theo tên khách hàng. 4. Chọn đơn hàng để xem chi tiết. 5. Cập nhật trạng thái nếu cần. |
| Exception conditions | Cập nhật trạng thái không hợp lệ (ví dụ: Paid → Pending): Hệ thống từ chối và hiển thị lý do. |

> **Triển khai:** [`OrderService.cs`](BLL/OrderService.cs), [`OrderRepository.cs`](DAL/Repositories/OrderRepository.cs), [`OrdersControl.cs`](BookStoreApp/UserControls/OrdersControl.cs). Các trạng thái hợp lệ: Pending → Paid → Cancelled. Đơn hàng đã Cancelled không thể mở lại; đơn hàng đã Paid không thể quay về Pending.

#### 3.3.5 Báo cáo & thống kê

Hệ thống báo cáo cung cấp các chỉ số thống kê về doanh thu, sách bán chạy, tồn kho và xuất báo cáo đa định dạng (CSV, Excel, văn bản).

| Thành phần | Nội dung mô tả |
|------------|----------------|
| Use case name | Báo cáo thống kê |
| Scenario | Quản lý xem báo cáo doanh thu, tồn kho, sách bán chạy |
| Triggering event | Người dùng chọn chức năng "Thống kê và báo cáo" trên hệ thống. |
| Brief description | Thực hiện thống kê, lập báo cáo các thông tin liên quan đến sách, khách hàng và tình hình bán hàng theo các tiêu chí được lựa chọn. |
| Actors | Admin |
| Preconditions | Người dùng đã đăng nhập. Hệ thống đang hoạt động bình thường. |
| Postconditions | Hiển thị báo cáo thống kê theo yêu cầu. Cung cấp tùy chọn xuất file báo cáo. |
| Flow of activities | 1. Người dùng chọn loại thống kê/báo cáo cần thực hiện. 2. Hệ thống xử lý yêu cầu và hiển thị kết quả thống kê/báo cáo. 3. Người dùng có thể xuất ra file CSV/Excel/văn bản. |
| Exception conditions | Không có dữ liệu trong kỳ được chọn. |

> **Triển khai:** [`ReportService.cs`](BLL/ReportService.cs), [`ReportRepository.cs`](DAL/Repositories/ReportRepository.cs), [`ReportsControl.cs`](BookStoreApp/UserControls/ReportsControl.cs). 7 loại báo cáo: Tổng quan doanh thu, Doanh thu theo ngày/tuần/tháng, Sách bán chạy, Sách bán chậm (90 ngày), Sách tồn kho thấp. Biểu đồ sử dụng thư viện OxyPlot. Xuất báo cáo: CSV (chuẩn), Excel (định dạng HTML), và văn bản (text).

#### 3.3.6 Đăng nhập và phân quyền

Hệ thống có module xác thực đăng nhập cơ bản với quy trình xác thực như sau:
- Giao diện đăng nhập tại [`LoginForm.cs`](BookStoreApp/Forms/LoginForm.cs).
- Người dùng nhập tên và mật khẩu.
- Gửi đến [`AuthService.cs`](BLL/AuthService.cs) sau đó kiểm tra thông tin qua [`AccountRepository.cs`](DAL/Repositories/AccountRepository.cs).
- Truy vấn bảng `Accounts` trong cơ sở dữ liệu.
- Nếu hợp lệ sẽ hiển thị giao diện chính.
- Nếu không hợp lệ hiển thị thông báo lỗi.

**Bảo mật hiện tại:** Thông tin tài khoản liên kết với `Employees`, cơ chế xác thực so sánh trực tiếp tên đăng nhập và mật khẩu. Trong phiên bản tiếp theo, nhóm dự kiến nâng cấp lên băm mật khẩu (BCrypt/SHA-256) để tăng cường bảo mật.

Hệ thống hỗ trợ hai vai trò chính:
- **Admin:** Toàn quyền truy cập tất cả chức năng.
- **Staff:** Chỉ được truy cập Dashboard, BookControl (chế độ chỉ đọc), POS, OrdersControl. Các chức năng quản lý danh mục, nhà cung cấp, khách hàng, nhân viên, báo cáo bị ẩn.

[`MainForm.cs`](BookStoreApp/Forms/MainForm.cs) kiểm tra `_role` trong constructor và điều chỉnh visibility của các nút sidebar. [`BookControl`](BookStoreApp/UserControls/BookControl.cs) nhận tham số `readOnly` để vô hiệu hóa các nút Thêm/Sửa/Xóa khi Staff truy cập.

**Tài khoản đăng nhập mặc định:**

| Username | Password | Vai trò |
|----------|----------|---------|
| `admin` | `1` | Admin (toàn quyền) |
| `E` | `2` | Staff (POS + xem kho) |

---

## CHƯƠNG IV: THIẾT KẾ GIAO DIỆN

### 4.1 Giao diện đăng nhập

Giao diện đăng nhập được thiết kế đơn giản, tập trung vào chức năng. Người dùng nhập tên đăng nhập và mật khẩu, sau đó nhấn nút "Login" để xác thực. Form có icon ứng dụng và logo thương hiệu.

> **Triển khai:** [`LoginForm.cs`](BookStoreApp/Forms/LoginForm.cs), [`LoginForm.Designer.cs`](BookStoreApp/Forms/LoginForm.Designer.cs). Theme áp dụng từ [`AppBranding.cs`](BookStoreApp/Theme/AppBranding.cs) và [`AppTheme.cs`](BookStoreApp/Theme/AppTheme.cs).

### 4.2 Giao diện chính

Giao diện chính ([`MainForm.cs`](BookStoreApp/Forms/MainForm.cs)) được tổ chức với thanh sidebar bên trái chứa các nút điều hướng và vùng nội dung chính bên phải hiển thị UserControl tương ứng. Theme sử dụng tông màu xanh dương đậm cho sidebar, trắng/xám cho nội dung chính.

Bao gồm các mục:
- **Dashboard:** Tổng quan với 8 chỉ số KPI, đơn hàng gần đây, sách bán chạy
- **Books:** Danh sách sách tồn kho với bộ lọc đa tiêu chí (danh mục, NXB, mức tồn kho, tìm kiếm)
- **Categories:** Thể loại sách hiện có trong nhà sách
- **Suppliers:** Thông tin nhà cung cấp
- **Customers:** Thông tin khách hàng và lịch sử mua hàng
- **Orders:** Thông tin đơn hàng với lọc theo ngày, trạng thái
- **Reports:** Báo cáo doanh thu, sách bán chạy, tồn kho với biểu đồ và xuất file
- **Employees:** Thông tin nhân viên (Chỉ hiển thị với tài khoản Admin)
- **POS:** Giao diện bán hàng tại quầy (mở từ Orders)
- **Nút Logout:** Đăng xuất khỏi tài khoản hiện tại

Hệ thống theme được quản lý tập trung qua [`AppTheme.cs`](BookStoreApp/Theme/AppTheme.cs) với bảng màu nhất quán:
- Sidebar: `#1F2937` (xanh đen)
- Active menu: `#2563EB` (xanh dương)
- Nền chính: `#F3F4F6` (xám nhạt)
- Header lưới: `#2563EB` (xanh dương)
- Nút Thêm: Xanh dương, Sửa: Cam, Xóa: Đỏ, Làm mới: Trắng/viền xám

---

## CHƯƠNG V: TRIỂN KHAI VÀ KIỂM THỬ

### 5.1 Công nghệ sử dụng

| Thành phần | Công nghệ |
|-----------|-----------|
| **Runtime** | .NET 9 |
| **UI Framework** | Windows Forms |
| **Ngôn ngữ** | C# 13 |
| **Database** | SQL Server (ADO.NET) |
| **ADO.NET Provider** | Microsoft.Data.SqlClient 5.2.2 |
| **Biểu đồ** | OxyPlot.WindowsForms 2.2.0 |
| **Cấu hình** | Microsoft.Extensions.Configuration.Json |
| **Payment APIs** | MoMo v2 sandbox, VNPay sandbox |

### 5.2 Kiến trúc hệ thống

Hệ thống được tổ chức thành 5 dự án trong cùng một solution (`BookStoreApp.sln`):

| Dự án | Vai trò | Thư mục |
|-------|--------|---------|
| **DTO** | Đối tượng truyền dữ liệu, enum, model | `DTO/` |
| **DAL** | Truy cập dữ liệu SQL Server qua ADO.NET | `DAL/` |
| **BLL** | Logic nghiệp vụ, kiểm tra hợp lệ, thanh toán | `BLL/` |
| **BookStoreApp** | Giao diện Windows Forms, ServiceLocator | `BookStoreApp/` |
| **Utilities** | Tiện ích: ghi log, xuất báo cáo | `Utilities/` |

**Luồng dữ liệu:** UI → BLL (Service) → DAL (Repository) → SQL Server

**Các mẫu thiết kế sử dụng:**
- **Repository Pattern:** Mỗi thực thể có interface repository riêng (VD: `IBookRepository`) và implementation SQL (VD: `BookRepository`)
- **Service Layer:** Mỗi nhóm nghiệp vụ có interface service (VD: `IBookService`) và implementation (VD: `BookService`)
- **Strategy Pattern:** Hệ thống thanh toán sử dụng `IPaymentProvider` với 3 implementation (MoMo, VNPay, Demo)
- **Factory Pattern:** `PaymentProviderFactory` tạo `IPaymentProvider` dựa trên cấu hình
- **Singleton:** Service và Repository được khởi tạo một lần trong `ServiceLocator`

### 5.3 Kiểm thử

Nhóm đã thực hiện kiểm thử hộp đen (black-box testing) trên các chức năng chính:

| Chức năng | Kịch bản kiểm thử | Kết quả |
|-----------|-------------------|---------|
| Đăng nhập | Nhập đúng sai tài khoản | ✓ Hiển thị thông báo phù hợp |
| CRUD Sách | Thêm/sửa/xóa sách | ✓ Hoạt động; xóa mềm |
| CRUD Khách hàng | Thêm/sửa/xóa khách hàng | ✓ Hoạt động |
| POS | Tạo giỏ hàng, áp dụng giảm giá, thanh toán | ✓ Tính toán chính xác |
| POS | Kiểm tra tồn kho khi thêm vào giỏ | ✓ Từ chối nếu vượt tồn kho |
| Thanh toán QR | Demo/MoMo/VNPay | ✓ Demo hoạt động; MoMo/VNPay cần credentials thật |
| Đơn hàng | Cập nhật trạng thái | ✓ Tuân thủ quy tắc nghiệp vụ |
| Báo cáo | 7 loại báo cáo, biểu đồ, xuất file | ✓ Hoạt động |
| Phân quyền | Staff bị ẩn chức năng quản trị | ✓ Sidebar ẩn đúng |

### 5.4 Các hạn chế đã biết

Trong quá trình phát triển, nhóm ghi nhận một số hạn chế cần khắc phục trong phiên bản tiếp theo:

1. **Cập nhật tồn kho sau bán hàng:** Logic trừ kho trong [`PosService.CompleteCheckout()`](BLL/PosService.cs) hiện tính toán đúng nhưng cần bổ sung lời gọi `_bookRepository.UpdateStock()` để lưu thay đổi vào cơ sở dữ liệu.
2. **Bảo mật mật khẩu:** Hiện tại mật khẩu được lưu dạng plaintext. Cần nâng cấp lên băm mật khẩu (BCrypt/SHA-256 + salt).
3. **Xuất báo cáo:** Xuất Excel hiện sử dụng định dạng HTML; xuất PDF hiện là định dạng văn bản. Cần tích hợp thư viện chuyên dụng (ClosedXML, PdfSharp) để tạo file chuẩn.
4. **Tìm kiếm:** Tìm kiếm sách hiện tải toàn bộ dữ liệu về bộ nhớ rồi lọc. Với số lượng sách lớn, cần chuyển sang tìm kiếm phía SQL Server.
5. **Xóa cứng:** Khách hàng và Nhân viên hiện sử dụng DELETE cứng, có thể gây lỗi tham chiếu. Cần chuyển sang xóa mềm như cách làm với Sách.
6. **Chưa có kiểm thử đơn vị:** Dự án chưa có dự án kiểm thử đơn vị (Unit Test). Cần bổ sung trong tương lai.
7. **Quản lý nhập kho:** Chưa có phân hệ ImportReceipt/ImportDetail riêng biệt. Hiện tại nhập kho thực hiện qua cập nhật trực tiếp số lượng sách.

---

## CHƯƠNG VI: KẾT LUẬN VÀ HƯỚNG PHÁT TRIỂN

### 6.1 Kết quả đạt được

Sau quá trình nghiên cứu và phát triển, nhóm đã xây dựng thành công một hệ thống quản lý nhà sách với các chức năng chính:

- ✅ **Xác thực và phân quyền:** Đăng nhập với hai vai trò Admin/Staff, giao diện tự động ẩn/hiện chức năng theo vai trò.
- ✅ **Quản lý sách:** CRUD đầy đủ với xóa mềm (soft delete), lọc đa tiêu chí (danh mục, NXB, mức tồn kho), tìm kiếm theo tên/tác giả/ISBN.
- ✅ **Quản lý danh mục và nhà cung cấp:** CRUD Categories và Suppliers.
- ✅ **Quản lý khách hàng:** CRUD khách hàng, xem lịch sử mua hàng, tích điểm thưởng (Loyalty Points).
- ✅ **Bán hàng POS:** Tạo giỏ hàng, giảm giá dòng và đơn hàng (phần trăm/số tiền), tính thuế, đổi điểm tích lũy, thanh toán tiền mặt hoặc QR.
- ✅ **Thanh toán QR:** Tích hợp 3 trình cung cấp — MoMo (HMAC-SHA256), VNPay (HMAC-SHA512), Demo (tự sinh QR bitmap). Cơ chế polling kiểm tra trạng thái thanh toán với đồng hồ đếm ngược.
- ✅ **Quản lý đơn hàng:** Xem danh sách, lọc theo ngày/trạng thái, xem chi tiết, cập nhật trạng thái với quy tắc nghiệp vụ.
- ✅ **Báo cáo & thống kê:** 7 loại báo cáo (tổng quan doanh thu, theo ngày/tuần/tháng, sách bán chạy, sách bán chậm, tồn kho thấp) với biểu đồ OxyPlot và xuất CSV/Excel/Văn bản.
- ✅ **Dashboard:** 8 chỉ số KPI, đơn hàng gần đây, sách bán chạy nhất.
- ✅ **Theme nhất quán:** Hệ thống màu sắc và style tập trung, áp dụng đồng bộ trên toàn bộ giao diện.
- ✅ **Ghi log:** Cơ chế ghi log theo ngày, thread-safe, ghi nhận các sự kiện quan trọng và lỗi.

### 6.2 Hạn chế

Bên cạnh những kết quả đạt được, hệ thống vẫn còn một số hạn chế:

1. Cần bổ sung lời gọi cập nhật tồn kho vào database sau mỗi giao dịch bán hàng.
2. Cần nâng cấp bảo mật mật khẩu từ plaintext lên băm.
3. Xuất báo cáo Excel/PDF cần được nâng cấp lên định dạng chuẩn.
4. Chưa có kiểm thử đơn vị tự động.
5. Quản lý nhập kho chưa có phân hệ riêng biệt.
6. Chưa có trình cài đặt (installer) để triển khai dễ dàng.
7. Giao diện chưa hỗ trợ đa ngôn ngữ.

### 6.3 Hướng phát triển

Trong tương lai, nhóm định hướng phát triển hệ thống theo các hướng sau:

- **Nâng cao bảo mật:** Triển khai băm mật khẩu (BCrypt), thêm giới hạn số lần đăng nhập sai, mã hóa dữ liệu nhạy cảm.
- **Hoàn thiện quản lý kho:** Xây dựng phân hệ ImportReceipt/ImportDetail để quản lý nhập kho chuyên nghiệp.
- **Mở rộng kênh bán hàng:** Tích hợp bán hàng online, đồng bộ tồn kho giữa online và offline.
- **Nâng cao trải nghiệm người dùng:** Hỗ trợ đa ngôn ngữ (Việt/Anh), phím tắt, in hóa đơn.
- **Kiểm thử tự động:** Xây dựng bộ kiểm thử đơn vị (Unit Test) và kiểm thử tích hợp (Integration Test).
- **Triển khai chuyên nghiệp:** Tạo trình cài đặt (MSI/ClickOnce), hỗ trợ cập nhật tự động.
- **Nâng cấp kiến trúc:** Chuyển từ Service Locator sang Dependency Injection container chính thống; tối ưu tìm kiếm phía SQL Server.

---

## TÀI LIỆU THAM KHẢO

1. Microsoft. (2024). *.NET 9 Documentation*. https://learn.microsoft.com/en-us/dotnet/
2. Microsoft. (2024). *Windows Forms Documentation*. https://learn.microsoft.com/en-us/dotnet/desktop/winforms/
3. Microsoft. (2024). *Microsoft.Data.SqlClient Documentation*. https://learn.microsoft.com/en-us/sql/connect/ado-net/microsoft-ADONET-sql-client
4. Microsoft. (2024). *SQL Server Documentation*. https://learn.microsoft.com/en-us/sql/
5. MoMo. (2024). *MoMo Payment API v2 Documentation*. https://developers.momo.vn/
6. VNPay. (2024). *VNPay Payment Gateway Integration Guide*. https://sandbox.vnpayment.vn/apis/
7. OxyPlot. (2024). *OxyPlot Documentation*. https://oxyplot.github.io/
8. Fowler, M. (2003). *Patterns of Enterprise Application Architecture*. Addison-Wesley.
9. Gamma, E., Helm, R., Johnson, R., & Vlissides, J. (1994). *Design Patterns: Elements of Reusable Object-Oriented Software*. Addison-Wesley.
10. Martin, R. C. (2008). *Clean Code: A Handbook of Agile Software Craftsmanship*. Prentice Hall.

---

<p style="text-align:center; color:#666; font-size:0.9em;">
  <em>Báo cáo được chỉnh sửa dựa trên kết quả kiểm toán độc lập ngày 07/06/2026.<br>
  Tất cả thông tin đã được đối chiếu với mã nguồn thực tế trong workspace.</em>
</p>
