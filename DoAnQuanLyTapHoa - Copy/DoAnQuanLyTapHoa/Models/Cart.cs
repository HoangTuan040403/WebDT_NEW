using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace DoAnQuanLyTapHoa.Models

{
    public class CartItem
    {
        //[Key] // Add this attribute to specify the primary key
        //public int CartId { get; set; }
        public SanPham _shopping_product { get; set; }
        public int _shopping_quantity { get; set; }
    }

    public class Cart
    {

        // Dùng List để lưu trữ giỏ hàng  là một bảng tạm
        List<CartItem> items = new List<CartItem>();
        public IEnumerable<CartItem> Items
        {
            get { return items; }
        }

        // Phương thức lấy sản phẩm bỏ vào giỏ hàng
        public void Add_Product_Cart(SanPham _pro, int _quantity = 1)
        {
            var item = Items.FirstOrDefault(s => s._shopping_product.MaSP == _pro.MaSP);
            if (item == null)
            {
                items.Add(new CartItem
                {
                    _shopping_product = _pro,
                    _shopping_quantity = _quantity
                });
            }
            else
            {
                item._shopping_quantity += _quantity;
            }
        }

        // Phương thức tính tổng số lượng trong giỏ hàng
        public int Total_quantity()
        {
            return items.Sum(s => s._shopping_quantity);
        }

        // Hàm tính thành tiền cho các sản phẩm trong giỏ hàng
        public decimal Total_money()
        {
            var total = items.Sum(s => s._shopping_quantity * s._shopping_product.GiaSP);
            return (decimal)total;
        }

        // Phương thức cập nhật số lượng khi SP mua thêm
        public void Update_quantity(string id, int _new_quan)
        {
            var item = items.Find(s => s._shopping_product.MaSP == id);
            if (item != null)
            {
                //item._shopping_quantity = item._shopping_quantity + _new_quan;
                item._shopping_quantity = _new_quan;

            }
        }

        // Phương thức xóa sản phẩm trong giỏ hàng
        public void Remove_CartItem(String id)
        {
            items.RemoveAll(s => s._shopping_product.MaSP == id);
        }
        public void ClearCart()
        {
            items.Clear();
        }
    }
}