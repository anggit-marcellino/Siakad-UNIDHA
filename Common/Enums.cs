using System;
using System.ComponentModel;

namespace Common
{
    public static class Enums
    {
        public static string GetEnumDescription(this Enum enumValue)
        {
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            var descriptionAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return descriptionAttributes.Length > 0 ? descriptionAttributes[0].Description : enumValue.ToString();
        }

        public enum TestEnum
        {
            [Description("Enum 1")]
            Enum1 = 1,
            [Description("Enum 2")]
            Enum2 = 2

        }

        // HCD
        public enum ContractEmployeeType
        {
            [Description("3 Months")]
            ThreeMonths = 1,
            [Description("6 Months")]
            SixMonths = 2,
            [Description("1 Year")]
            OneYear = 3
        }

        // Sales
        public enum DealTypeEnum
        {
            [Description("NewBusiness")]
            NewBusiness = 0,
            [Description("ExistingBusiness")]
            ExistingBusiness = 1
        }

        public enum DealPriorityEnum
        {
            [Description("Low")]
            Low = 0,
            [Description("Medium")]
            Medium = 1,
            [Description("High")]
            High = 2
        }

        //Finance
        public enum AprrovalStatusEnum
        {
            [Description("WaitingForAprroval")]
            WaitingForAprroval = 0,
            [Description("Aproved")]
            Aproved = 1,
            [Description("Rejected")]
            Rejected = 2
        }

        public enum PaymentStatusEnum
        {
            [Description("Paid")]
            Paid = 1,
            [Description("Unpaid")]
            Unpaid = 2
        }

    }
    //public static class EnumExtensionMethods
    //{
    //    public static string GetEnumDescription(this Enum enumValue)
    //    {
    //        var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

    //        var descriptionAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

    //        return descriptionAttributes.Length > 0 ? descriptionAttributes[0].Description : enumValue.ToString();
    //    }
    //}
}
