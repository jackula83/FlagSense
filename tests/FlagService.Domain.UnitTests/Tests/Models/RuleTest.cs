using FlagService.Domain.Aggregates;
using FlagService.Domain.Entities.Rules;
using Framework2.Core.Extensions;
using System.Collections.Generic;
using Xunit;

namespace FlagSense.FlagService.UnitTests.Domain.Models
{
    public class RuleTest
    {
        [Theory]
        [MemberData(nameof(ProvideOneOfRuleData))]
        [MemberData(nameof(ProvideStartWithRuleData))]
        [MemberData(nameof(ProvideRegexRuleData))]
        public void Eval_ProvidedRule_TrueWhenConditionMatchesUser(User user, Rule rule, bool expectedResult)
        {
            // act
            var result = rule.EvaluateUserAttributes(user);

            // assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [MemberData(nameof(ProvideOneOfRuleData))]
        [MemberData(nameof(ProvideStartWithRuleData))]
        [MemberData(nameof(ProvideRegexRuleData))]
        public void Eval_ProvidedRule_TrueWhenConditionMatchesAttributes(User user, Rule rule, bool expectedResult)
        {
            // act
            var result = rule.EvaluateAttributes(user.Attributes);

            // assert
            Assert.Equal(expectedResult, result);
        }

        public static IEnumerable<object[]> ProvideOneOfRuleData()
        {
            yield return new object[]
            {
                new User()
                    .Tap(x => x.Attributes.Add(new("username", "a"))),
                new Rule()
                    .Tap(x => x.Conditions.Add(new()
                    {
                        AttributeName = "username",
                        Operator = ConditionOperator.ONE_OF,
                        Value = "a, b, c"
                    })),
                true
            };
            yield return new object[]
            {
                new User()
                    .Tap(x => x.Attributes.Add(new("username", "a"))),
                new Rule()
                    .Tap(x => x.Conditions.Add(new()
                    {
                        AttributeName = "username",
                        Operator = ConditionOperator.ONE_OF,
                        Value = "b, c"
                    })),
                false
            };
            yield return new object[]
            {
                new User()
                    .Tap(x => x.Attributes.Add(new("username", "a"))),
                new Rule()
                    .Tap(x => x.Conditions.Add(new()
                    {
                        AttributeName = "country",
                        Operator = ConditionOperator.ONE_OF,
                        Value = "a, b, c"
                    })),
                false
            };
        }

        public static IEnumerable<object[]> ProvideStartWithRuleData()
        {
            yield return new object[]
            {
                new User()
                    .Tap(x => x.Attributes.Add(new("username", "jackula"))),
                new Rule()
                    .Tap(x => x.Conditions.Add(new()
                    {
                        AttributeName = "username",
                        Operator = ConditionOperator.STARTS_WITH,
                        Value = "jack"
                    })),
                true
            };
            yield return new object[]
            {
                new User()
                    .Tap(x => x.Attributes.Add(new("username", "jackula"))),
                new Rule()
                    .Tap(x => x.Conditions.Add(new()
                    {
                        AttributeName = "username",
                        Operator = ConditionOperator.STARTS_WITH,
                        Value = "melon"
                    })),
                false
            };
            yield return new object[]
            {
                new User()
                    .Tap(x => x.Attributes.Add(new("username", "jackula"))),
                new Rule()
                    .Tap(x => x.Conditions.Add(new()
                    {
                        AttributeName = "username",
                        Operator = ConditionOperator.STARTS_WITH,
                        Value = "ula"
                    })),
                false
            };
        }

        public static IEnumerable<object[]> ProvideRegexRuleData()
        {
            yield return new object[]
            {
                new User()
                    .Tap(x => x.Attributes.Add(new("username", "jackula@nospam.com"))),
                new Rule()
                    .Tap(x => x.Conditions.Add(new()
                    {
                        AttributeName = "username",
                        Operator = ConditionOperator.REGEX,
                        Value = @"([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)"
                    })),
                true
            };
            yield return new object[]
            {
                new User()
                    .Tap(x => x.Attributes.Add(new("username", "jackula"))),
                new Rule()
                    .Tap(x => x.Conditions.Add(new()
                    {
                        AttributeName = "username",
                        Operator = ConditionOperator.REGEX,
                        Value = @"([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)"
                    })),
                false
            };
            yield return new object[]
            {
                new User()
                    .Tap(x => x.Attributes.Add(new("username", "jackula@nospam.com"))),
                new Rule()
                    .Tap(x => x.Conditions.Add(new()
                    {
                        AttributeName = "username",
                        Operator = ConditionOperator.REGEX,
                        Value = @"["
                    })),
                false
            };
            yield return new object[]
            {
                new User()
                    .Tap(x => x.Attributes.Add(new("username", "jackula@nospam.com"))),
                new Rule()
                    .Tap(x => x.Conditions.Add(new()
                    {
                        AttributeName = "username",
                        Operator = ConditionOperator.REGEX,
                        Value = @"^[a-z]$"
                    })),
                false
            };
        }
    }
}
