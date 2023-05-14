using Cafetown.Common;

namespace Cafetown.DL.UnitTests
{
    public class Tests
    {
        /// <summary>
        /// Mã bị trùng
        /// </summary>
        /// Modified by: TTTuan 1/5/2023
        [Test]
        public void CheckDuplicateCode_DuplicateCode_ReturnsTrue()
        {
            // Arrange - Chuẩn bị dữ liệu đầu vào và kết quả mong muốn 
            var fakeConnectionDL = new FakeConnectionDL();
            var baseDL = new BaseDL<Employee>(fakeConnectionDL);

            // Act - Gọi vào hàm cần test
            var actualResult = baseDL.CheckDuplicateCode(null, "NV-08449");

            // Assert - Kiểm tra kết quả mong muốn và kết quả thực tế
            Assert.That(actualResult, Is.EqualTo(true));
        }

        /// <summary>
        /// Thêm nhân viên thành công
        /// </summary>
        /// Modified by: TTTuan 1/5/2023
        [Test]
        public void InsertRecord_Employee_Returns1()
        {
            // Arrange - Chuẩn bị dữ liệu đầu vào và kết quả mong muốn
            var e = new Employee()
            {
                EmployeeCode = "NV-1111",
                EmployeeName = "Trần Thái Tuấn",
                JobPosition = null,
                DateOfBirth = DateTime.Now
            };

            var fakeConnectionDL = new FakeConnectionDL();
            var baseDL = new BaseDL<Employee>(fakeConnectionDL);

            // Act - Gọi vào hàm cần test
            var actualResult = baseDL.InsertRecord(e);

            // Assert - Kiểm tra kết quả mong muốn và kết quả thực tế
            Assert.That(actualResult, Is.EqualTo(1));
        }

        /// <summary>
        /// Xóa nhân viên thành công
        /// </summary>
        /// Modified by: TTTuan 1/5/2023
        [Test]
        public void UpdateRecordByID_EmployeeID_Returns1()
        {
            // Arrange - Chuẩn bị dữ liệu đầu vào và kết quả mong muốn
            var e = new Employee()
            {
                EmployeeCode = "NV-1111",
                EmployeeName = "Trần Thái Tuấn",
                JobPosition = null,
                DateOfBirth = DateTime.Now
            };

            var fakeConnectionDL = new FakeConnectionDL();
            var baseDL = new BaseDL<Employee>(fakeConnectionDL);

            // Act - Gọi vào hàm cần test
            var actualResult = baseDL.UpdateRecordByID(new Guid("7686595d-16d5-33b3-0080-e8e2a817c80e"), e);

            // Assert - Kiểm tra kết quả mong muốn và kết quả thực tế
            Assert.That(actualResult, Is.EqualTo(1));
        }
    }
}