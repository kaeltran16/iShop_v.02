//using System;
//using iShop.Common.Helpers;

//namespace iShop.Common.Extensions
//{
//    public static class LoggerExtension
//    {
//        public static void LogMessage(this ILogger logger, int code, string itemName, Guid itemId)
//        {
//            switch (code)
//            {
//                case LoggingResult.SavedFail:
//                    logger.LogError(LoggingResult.Fail, itemName + " with id " + itemId + " failed to saved");
//                    break;
//                case LoggingResult.Created:
//                    logger.LogInformation(LoggingResult.Created, itemName + " with id " + itemId + " has been created");
//                    break;
//                case LoggingResult.Deleted:
//                    logger.LogError(LoggingResult.Deleted, itemName + " with id " + itemId + " has been deleted");
//                    break;
//                case LoggingResult.Updated:
//                    logger.LogInformation(LoggingResult.Updated, itemName + " with id " + itemId + " has been updated");
//                    break;
//                default: 
//                    throw new ArgumentException("code is invalid");
//            }
//        }
//    }
//}
