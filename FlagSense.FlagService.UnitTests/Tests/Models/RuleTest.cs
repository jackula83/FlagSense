using Common.Domain.Core.Extensions;
using FlagSense.FlagService.Domain.Entities;
using FlagService.Domain.Aggregates.RuleGroup;
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
        public void Eval_ProvidedRule_TrueWhenConditionMatches(User user, Rule rule, bool expectedResult)
        {
            // act
            var result = rule.EvalulateUserFlags(user);

            // assert
            Assert.Equal(expectedResult, result);
        }

        public static IEnumerable<object[]> ProvideOneOfRuleData()
        {
            yield return new object[]
            {
                new User()
                    .Tap(x => x.Properties.Add(new("username", "a"))),
                new Rule()
                    .Tap(x => x.RuleType = FlagRuleType.ONE_OF)
                    .Tap(x => x.Key = "username")
                    .Tap(x => x.Conditions.AddRange(new Condition[] { "a", "b", "c" })),
                true
            };
            yield return new object[]
            {
                new User()
                    .Tap(x => x.Properties.Add(new("username", "a"))),
                new Rule()
                    .Tap(x => x.RuleType = FlagRuleType.ONE_OF)
                    .Tap(x => x.Key = "username")
                    .Tap(x => x.Conditions.AddRange(new Condition[] { "b", "c" })),
                false
            };
            yield return new object[]
            {
                new User()
                    .Tap(x => x.Properties.Add(new("username", "a"))),
                new Rule()
                    .Tap(x => x.RuleType = FlagRuleType.ONE_OF)
                    .Tap(x => x.Key = "country")
                    .Tap(x => x.Conditions.AddRange(new Condition[] { "a", "b", "c" })),
                false
            };
        }

        public static IEnumerable<object[]> ProvideStartWithRuleData()
        {
            yield return new object[]
            {
                new User()
                    .Tap(x => x.Properties.Add(new("username", "jackula"))),
                new Rule()
                    .Tap(x => x.RuleType = FlagRuleType.STARTS_WITH)
                    .Tap(x => x.Key = "username")
                    .Tap(x => x.Conditions.AddRange(new Condition[] { "melon", "jack" })),
                true
            };
            yield return new object[]
            {
                new User()
                    .Tap(x => x.Properties.Add(new("username", "jackula"))),
                new Rule()
                    .Tap(x => x.RuleType = FlagRuleType.STARTS_WITH)
                    .Tap(x => x.Key = "username")
                    .Tap(x => x.Conditions.AddRange(new Condition[] { "melon" })),
                false
            };
            yield return new object[]
            {
                new User()
                    .Tap(x => x.Properties.Add(new("username", "jackula"))),
                new Rule()
                    .Tap(x => x.RuleType = FlagRuleType.STARTS_WITH)
                    .Tap(x => x.Key = "username")
                    .Tap(x => x.Conditions.AddRange(new Condition[] { "ula" })),
                false
            };
        }

        public static IEnumerable<object[]> ProvideRegexRuleData()
        {
            yield return new object[]
            {
                new User()
                    .Tap(x => x.Properties.Add(new("username", "jackula@nospam.com"))),
                new Rule()
                    .Tap(x => x.RuleType = FlagRuleType.REGEX)
                    .Tap(x => x.Key = "username")
                    .Tap(x => x.Conditions.AddRange(new Condition[] {  @"([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)" })),
                true
            };
            yield return new object[]
            {
                new User()
                    .Tap(x => x.Properties.Add(new("username", "jackula"))),
                new Rule()
                    .Tap(x => x.RuleType = FlagRuleType.REGEX)
                    .Tap(x => x.Key = "username")
                    .Tap(x => x.Conditions.AddRange(new Condition[] {  @"([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)" })),
                false
            };
            yield return new object[]
            {
                new User()
                    .Tap(x => x.Properties.Add(new("username", "jackula@nospam.com"))),
                new Rule()
                    .Tap(x => x.RuleType = FlagRuleType.REGEX)
                    .Tap(x => x.Key = "username")
                    .Tap(x => x.Conditions.AddRange(new Condition[] {  @"[" })),
                false
            };
            yield return new object[]
            {
                new User()
                    .Tap(x => x.Properties.Add(new("username", "jackula@nospam.com"))),
                new Rule()
                    .Tap(x => x.RuleType = FlagRuleType.REGEX)
                    .Tap(x => x.Key = "username")
                    .Tap(x => x.Conditions.AddRange(new Condition[] {  @"^[a-z]$" })),
                false
            };
        }
    }
}
