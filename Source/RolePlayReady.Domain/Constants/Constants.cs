namespace RolePlayReady.Constants;

public static class Constants {
    public const string InternalUser = "Internal";

    public static class Validation {
        public static class Definition {
            public const int MaximumNameLength = 100;
            public const int MinimumNameLength = 3;
            public const int MaximumDescriptionLength = 1000;
            public const int MinimumDescriptionLength = 10;
            public const int MaximumShortNameLength = 10;
            public const int MinimumShortNameLength = 2;
            public const int MaximumTagLength = 20;
        }
    }
}
