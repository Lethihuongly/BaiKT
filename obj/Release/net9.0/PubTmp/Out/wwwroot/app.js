const apiUrl = "http://localhost:7153/api/Goods"; // API Backend chính xác

// Lấy danh sách hàng hóa từ API
async function fetchGoods() {
    try {
        const response = await fetch(apiUrl);
        if (!response.ok) throw new Error(`Lỗi khi lấy dữ liệu: ${response.status}`);

        const goods = await response.json();
        const table = document.querySelector("#goodsTable tbody");
        table.innerHTML = "";

        goods.forEach(g => {
            const row = document.createElement("tr");
            row.innerHTML = `
                <td>${g.MaHangHoa}</td>
                <td>${g.TenHangHoa}</td>
                <td>${g.SoLuong}</td>
                <td contenteditable="true" onblur="updateGhiChu('${g.MaHangHoa}', this.innerText)">
                    ${g.GhiChu || "Không có ghi chú"}
                </td>
                <td>
                    <button class="edit" onclick="editGood('${g.MaHangHoa}', '${g.TenHangHoa}', ${g.SoLuong}, '${g.GhiChu || ""}')">✏️</button>
                    <button class="delete" onclick="deleteGood('${g.MaHangHoa}')">🗑️</button>
                </td>
            `;
            table.appendChild(row);
        });

        resetForm();
    } catch (error) {
        console.error("Lỗi:", error);
        alert("Không thể tải danh sách hàng hóa! Kiểm tra API backend.");
    }
}

// Thêm hàng hóa mới
async function createGood() {
    const MaHangHoa = document.querySelector("#MaHangHoa").value.trim();
    const TenHangHoa = document.querySelector("#TenHangHoa").value.trim();
    const SoLuong = document.querySelector("#SoLuong").value.trim();
    const GhiChu = document.querySelector("#GhiChu").value.trim();

    if (!MaHangHoa || !TenHangHoa || !SoLuong) {
        alert("Vui lòng nhập mã hàng hóa, tên và số lượng!");
        return;
    }

    const newGood = { MaHangHoa, TenHangHoa, SoLuong: parseInt(SoLuong), GhiChu };

    try {
        const response = await fetch(apiUrl, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(newGood),
        });

        if (!response.ok) {
            const errorText = await response.text();
            throw new Error(`Lỗi khi thêm hàng hóa: ${response.status} - ${errorText}`);
        }
        alert("✅ Thêm hàng hóa thành công!");
        fetchGoods();
    } catch (error) {
        console.error("Lỗi:", error);
        alert("❌ Không thể thêm hàng hóa! " + error.message);
    }
}

// Chỉnh sửa hàng hóa
function editGood(MaHangHoa, TenHangHoa, SoLuong, GhiChu) {
    document.querySelector("#MaHangHoa").value = MaHangHoa;
    document.querySelector("#TenHangHoa").value = TenHangHoa;
    document.querySelector("#SoLuong").value = SoLuong;
    document.querySelector("#GhiChu").value = GhiChu;

    document.querySelector(".update-btn").style.display = "inline-block";
    document.querySelector(".add-btn").style.display = "none";
}

// Cập nhật hàng hóa
async function updateGood() {
    const MaHangHoa = document.querySelector("#MaHangHoa").value.trim();
    const TenHangHoa = document.querySelector("#TenHangHoa").value.trim();
    const SoLuong = document.querySelector("#SoLuong").value.trim();
    const GhiChu = document.querySelector("#GhiChu").value.trim();

    if (!MaHangHoa || !TenHangHoa || !SoLuong) {
        alert("Vui lòng nhập đầy đủ thông tin!");
        return;
    }

    const updatedGood = { MaHangHoa, TenHangHoa, SoLuong: parseInt(SoLuong), GhiChu };

    try {
        const response = await fetch(`${apiUrl}/${MaHangHoa}`, {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(updatedGood),
        });

        if (!response.ok) throw new Error(`Lỗi khi cập nhật: ${response.status}`);
        alert("✅ Cập nhật thành công!");
        fetchGoods();
    } catch (error) {
        console.error("Lỗi:", error);
        alert("❌ Không thể cập nhật hàng hóa!");
    }
}

// Xóa hàng hóa
async function deleteGood(MaHangHoa) {
    if (!confirm("Bạn có chắc muốn xóa hàng hóa này?")) return;

    try {
        const response = await fetch(`${apiUrl}/${MaHangHoa}`, { method: "DELETE" });

        if (!response.ok) throw new Error(`Lỗi khi xóa: ${response.status}`);
        alert("🗑️ Xóa thành công!");
        fetchGoods();
    } catch (error) {
        console.error("Lỗi:", error);
        alert("❌ Không thể xóa hàng hóa!");
    }
}

// Cập nhật ghi chú nhanh (inline edit)
async function updateGhiChu(MaHangHoa, GhiChu) {
    try {
        const response = await fetch(`${apiUrl}/update-note/${MaHangHoa}`, {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ GhiChu }),
        });

        if (!response.ok) throw new Error(`Lỗi khi cập nhật ghi chú: ${response.status}`);
        alert("📝 Cập nhật ghi chú thành công!");
    } catch (error) {
        console.error("Lỗi:", error);
        alert("❌ Không thể cập nhật ghi chú!");
    }
}

// Reset form sau khi cập nhật/thêm hàng hóa
function resetForm() {
    document.querySelector("#MaHangHoa").value = "";
    document.querySelector("#TenHangHoa").value = "";
    document.querySelector("#SoLuong").value = "";
    document.querySelector("#GhiChu").value = "";

    document.querySelector(".update-btn").style.display = "none";
    document.querySelector(".add-btn").style.display = "inline-block";
}

// Tải danh sách khi trang load
document.addEventListener("DOMContentLoaded", fetchGoods);
