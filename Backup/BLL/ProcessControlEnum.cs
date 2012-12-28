using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{

    public enum ProcessControl
    {
        初始化 = 0,
        登陆 = 1,
        打开菜单 = 2,
        打开子菜单 = 3,
        父菜单 = 4,
        报错 = 5,
        填充数据 = 6,
        提交 = 7,
        完成 = 8
    }

    public enum FillData
    {
        初始化 = 0,
        填写会员卡号 = 1,
        填写PNR = 2,
        是否导入成功 = 3
    }

}
