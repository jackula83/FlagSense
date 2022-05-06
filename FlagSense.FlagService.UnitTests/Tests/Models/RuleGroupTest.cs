using FlagSense.FlagService.Domain.Entities;
using FlagService.Domain.Aggregates.RuleGroup;
using Framework2.Core.Extensions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FlagSense.FlagService.UnitTests.Domain.Models
{
    public class RuleGroupTest
    {

        [Fact]
        public void Eval_WhenAllRulesEvalTrue_WillEvalTrue()
        {
            // arrange
            var user = new User().Tap(x => x.Properties.Add(new("username", "jackula@nospam.com")));
            var ruleGroup = new RuleGroup().Tap(x => x.Rules.AddRange(GetTrueRules()));

            // act
            var result = ruleGroup.EvalulateUserFlags(user);

            // assert
            Assert.True(result);
        }

        [Fact]
        public void Eval_WhenAllRulesEvalFalse_WillEvalFalse()
        {
            // arrange
            var user = new User().Tap(x => x.Properties.Add(new("username", "jackula@nospam.com")));
            var ruleGroup = new RuleGroup().Tap(x => x.Rules.AddRange(GetFalseRules()));

            // act
            var result = ruleGroup.EvalulateUserFlags(user);

            // assert
            Assert.False(result);
        }

        [Fact]
        public void Eval_WhenOneRuleEvalFalse_WillEvalFalse()
        {
            // arrange
            var user = new User().Tap(x => x.Properties.Add(new("username", "jackula@nospam.com")));
            var ruleGroup = new RuleGroup()
                .Tap(x => x.Rules.AddRange(GetTrueRules()))
                .Tap(x => x.Rules.Add(GetFalseRules().First()));

            // act
            var result = ruleGroup.EvalulateUserFlags(user);

            // assert
            Assert.False(result);
        }

        private static List<Rule> GetTrueRules()
        {
            var rule1 = new Rule()
                .Tap(x => x.Key = "username")
                .Tap(x => x.RuleType = FlagRuleType.ONE_OF)
                .Tap(x => x.Conditions.Add("jackula@nospam.com"));
            var rule2 = new Rule()
                .Tap(x => x.Key = "username")
                .Tap(x => x.RuleType = FlagRuleType.STARTS_WITH)
                .Tap(x => x.Conditions.Add("jack"));
            var rule3 = new Rule()
                .Tap(x => x.Key = "username")
                .Tap(x => x.RuleType = FlagRuleType.REGEX)
                .Tap(x => x.Conditions.Add(@"([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)"));
            return new() { rule1, rule2, rule3 };
        }

        private static List<Rule> GetFalseRules()
        {
            var rule1 = new Rule()
                .Tap(x => x.Key = "username")
                .Tap(x => x.RuleType = FlagRuleType.ONE_OF)
                .Tap(x => x.Conditions.Add("jackula"));
            var rule2 = new Rule()
                .Tap(x => x.Key = "username")
                .Tap(x => x.RuleType = FlagRuleType.STARTS_WITH)
                .Tap(x => x.Conditions.Add("nospam"));
            var rule3 = new Rule()
                .Tap(x => x.Key = "username")
                .Tap(x => x.RuleType = FlagRuleType.REGEX)
                .Tap(x => x.Conditions.Add(@"["));
            return new() { rule1, rule2, rule3 };
        }
    }
}
