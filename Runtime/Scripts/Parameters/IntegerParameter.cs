﻿using System;
using Helluys.FsmCore.Serialization;

namespace Helluys.FsmCore.Parameters
{
    public class IntegerParameter : FsmParameter
    {
        public int value;

        public int Get () {
            return value;
        }

        public void Set (int newValue) {
            value = newValue;
        }

        public override bool Equals (object other) {
            if (other == this) {
                return true;
            } else if (other is IntegerParameter) {
                return (other as IntegerParameter).value == value;
            } else {
                return false;
            }
        }

        public override int GetHashCode () {
            return value.GetHashCode();
        }

        public override bool Equals (FsmConstant constant) {
            if (constant is ConstantIntegerParameter) {
                return value == (constant as ConstantIntegerParameter).value;
            } else {
                throw new NotSupportedException();
            }
        }

        public override bool GreaterThan (FsmConstant constant) {
            if (constant is ConstantIntegerParameter) {
                return value > (constant as ConstantIntegerParameter).value;
            } else {
                throw new NotSupportedException();
            }
        }

        public override bool SmallerThan (FsmConstant constant) {
            if (constant is ConstantIntegerParameter) {
                return value < (constant as ConstantIntegerParameter).value;
            } else {
                throw new NotSupportedException();
            }
        }

        public override SerializableFsmParameter Serialize (string name) {
            return new SerializableFsmParameter() {
                name = name,
                type = SerializableFsmParameter.Type.INTEGER,
                defaultValue = new ConstantIntegerParameter() {
                    value = value
                }.Serialize()
            };
        }
    }
}