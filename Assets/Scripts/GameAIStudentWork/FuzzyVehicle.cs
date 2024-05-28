// Remove the line above if you are submitting to GradeScope for a grade. But leave it if you only want to check
// that your code compiles and the autograder can access your public methods.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameAI;

// All the Fuzz
using Tochas.FuzzyLogic;
using Tochas.FuzzyLogic.MembershipFunctions;
using Tochas.FuzzyLogic.Evaluators;
using Tochas.FuzzyLogic.Mergers;
using Tochas.FuzzyLogic.Defuzzers;
using Tochas.FuzzyLogic.Expressions;

namespace GameAICourse
{

    public class FuzzyVehicle : AIVehicle
    {

        // TODO create some Fuzzy Set enumeration types, and member variables for:
        // Fuzzy Sets (input and output), one or more Fuzzy Value Sets, and Fuzzy
        // Rule Sets for each output.
        // Also, create some methods to instantiate each of the member variables

        // Here are some basic examples to get you started

        enum FzInputSpeed {Slow, Medium, Fast} //Vehicle Speed
        enum FzOutputWheel {TurnLeft, Straight, TurnRight} //Desired Turn
        enum FzVehiclePosition {Left, Center, Right}// Vehicle Position
        enum FzVehicleDirection {Left, Center, Right} //Vehicle Direction
        enum FzOutputThrottle {Brake, Coast, Accelerate} //Speed you want the vehicle to achieve

        FuzzySet<FzInputSpeed> fzSpeedSet;
        FuzzySet<FzOutputWheel> fzWheelSet;
        FuzzySet<FzOutputThrottle> fzThrottleSet;

        FuzzySet<FzVehiclePosition> fzVehiclePositionSet; 

        FuzzySet<FzVehicleDirection> fzVehicleDirectionSet; 

        FuzzyRuleSet<FzOutputThrottle> fzThrottleRuleSet; 

        FuzzyRuleSet<FzOutputWheel> fzWheelRuleSet; 

        FuzzyValueSet fzInputValueSet = new FuzzyValueSet();


        // These are used for debugging (see ApplyFuzzyRules() call
        // in Update()
        FuzzyValueSet mergedThrottle = new FuzzyValueSet();
        FuzzyValueSet mergedWheel = new FuzzyValueSet();


        //vehicle speed

        private FuzzySet<FzInputSpeed> GetSpeedSet()
        {
            FuzzySet<FzInputSpeed> set = new FuzzySet<FzInputSpeed>();

            // TODO: Add some membership functions for each state
            IMembershipFunction SlowMemberShip = new ShoulderMembershipFunction(0f, new Coords(0f, 1f), new Coords(40f, 0f), 80f);
            IMembershipFunction MediumMemberShip = new TriangularMembershipFunction(new Coords(35f, 0f), new Coords(45f, 1f), new Coords(55f, 0f));
            IMembershipFunction FastMemberShip = new ShoulderMembershipFunction(0f, new Coords(55f, 0f), new Coords(80f, 1f), 80f);
            
            set.Set(new FuzzyVariable<FzInputSpeed>(FzInputSpeed.Slow, SlowMemberShip));
            set.Set(new FuzzyVariable<FzInputSpeed>(FzInputSpeed.Medium, MediumMemberShip));
            set.Set(new FuzzyVariable<FzInputSpeed>(FzInputSpeed.Fast, FastMemberShip));
            return set;

        }

        private FuzzySet<FzVehiclePosition> GetVehiclePosSet() 
        {
            IMembershipFunction leftMembershipFunction = new ShoulderMembershipFunction(-1.5f, new Coords(-1.5f, 1f), new Coords(-1f, 0f), 1.5f);
            IMembershipFunction centerMembershipFunction = new TriangularMembershipFunction(new Coords(-1.5f, 0f), new Coords(0f, 1f), new Coords(1.5f, 0f));
            IMembershipFunction rightMembershipFunction = new ShoulderMembershipFunction(-2f, new Coords(1f, 0f), new Coords(2f, 1f), 2f);           
            
            FuzzySet<FzVehiclePosition> set = new FuzzySet<FzVehiclePosition>();
            set.Set(new FuzzyVariable<FzVehiclePosition>(FzVehiclePosition.Left, leftMembershipFunction));
            set.Set(new FuzzyVariable<FzVehiclePosition>(FzVehiclePosition.Center, centerMembershipFunction));
            set.Set(new FuzzyVariable<FzVehiclePosition>(FzVehiclePosition.Right, rightMembershipFunction));
            return set;
        }

        private FuzzySet<FzVehicleDirection> GetVehicleDirSet()
        {

            IMembershipFunction leftMembershipFunction = new ShoulderMembershipFunction(25f, new Coords(25f, 1f), new Coords(5f, 0f), -25f);
            IMembershipFunction centerMembershipFunction = new TriangularMembershipFunction(new Coords(25f, 0f), new Coords(0f, 1f), new Coords(-25f, 0f));
            IMembershipFunction rightMembershipFunction = new ShoulderMembershipFunction(25f, new Coords(-5f, 0f), new Coords(-25f, 1f), -25f);

            FuzzySet<FzVehicleDirection> set = new FuzzySet<FzVehicleDirection>();
            set.Set(new FuzzyVariable<FzVehicleDirection>(FzVehicleDirection.Left, leftMembershipFunction));
            set.Set(new FuzzyVariable<FzVehicleDirection>(FzVehicleDirection.Center, centerMembershipFunction));
            set.Set(new FuzzyVariable<FzVehicleDirection>(FzVehicleDirection.Right, rightMembershipFunction));
            return set;
        }


        private FuzzySet<FzOutputThrottle> GetThrottleSet()
        {

            FuzzySet<FzOutputThrottle> set = new FuzzySet<FzOutputThrottle>();


            // TODO: Add some membership functions for each state
            IMembershipFunction brakeMembershipFunction = new ShoulderMembershipFunction(-1f, new Coords(-0.3f, 0f), new Coords(-0.1f, 0f), 0f);
            IMembershipFunction coastMembershipFunction = new TriangularMembershipFunction(new Coords(-0.5f, 0f), new Coords(0.5f, 1f), new Coords(1.5f, 0f));
            IMembershipFunction accelerateMembershipFunction = new ShoulderMembershipFunction(-1f, new Coords(0.5f, 1f), new Coords(1f, 1f), 1f);



            set.Set(FzOutputThrottle.Brake, brakeMembershipFunction);
            set.Set(FzOutputThrottle.Coast, coastMembershipFunction);
            set.Set(FzOutputThrottle.Accelerate, accelerateMembershipFunction);

            return set;
        }

        private FuzzySet<FzOutputWheel> GetWheelSet()
        {

            FuzzySet<FzOutputWheel> set = new FuzzySet<FzOutputWheel>();

            IMembershipFunction leftMembershipFunction = new ShoulderMembershipFunction(-0.8f, new Coords(-0.8f, 1f), new Coords(-0.6f, 0f), 0.8f);
            IMembershipFunction centerMembershipFunction = new TriangularMembershipFunction(new Coords(-0.8f, 0f), new Coords(0f, 1f), new Coords(0.8f, 0f));
            IMembershipFunction rightMembershipFunction = new ShoulderMembershipFunction(-0.8f, new Coords(0.6f, 0f), new Coords(0.8f, 0.9f), 0.8f);


            set.Set(FzOutputWheel.TurnLeft, leftMembershipFunction);
            set.Set(FzOutputWheel.Straight, centerMembershipFunction);
            set.Set(FzOutputWheel.TurnRight, rightMembershipFunction);
            return set;
        }


        private FuzzyRule<FzOutputThrottle>[] GetThrottleRules()
        {

            FuzzyRule<FzOutputThrottle>[] rules =
            {
                // TODO: Add some rules. Here is an example
                // (Note: these aren't necessarily good rules)
                If(FzInputSpeed.Slow).Then(FzOutputThrottle.Accelerate),
                If(FzInputSpeed.Medium).Then(FzOutputThrottle.Coast),
                If(FzInputSpeed.Fast).Then(FzOutputThrottle.Brake),
                // More example syntax
                //If(And(FzInputSpeed.Fast, Not(FzFoo.Bar)).Then(FzOutputThrottle.Accelerate),
            };

            return rules;
        }

        private FuzzyRule<FzOutputWheel>[] GetWheelRules()
        {

            FuzzyRule<FzOutputWheel>[] rules =
            {
                // TODO: Add some rules.
                If(FzVehicleDirection.Left).Then(FzOutputWheel.TurnRight),
                If(FzVehicleDirection.Center).Then(FzOutputWheel.Straight),
                If(FzVehicleDirection.Right).Then(FzOutputWheel.TurnLeft),

                If(FzVehiclePosition.Left).Then(FzOutputWheel.TurnRight),
                If(FzVehiclePosition.Center).Then(FzOutputWheel.Straight),
                If(FzVehiclePosition.Right).Then(FzOutputWheel.TurnLeft),

            };

            return rules;
        }

        private FuzzyRuleSet<FzOutputThrottle> GetThrottleRuleSet(FuzzySet<FzOutputThrottle> throttle)
        {
            var rules = this.GetThrottleRules();
            return new FuzzyRuleSet<FzOutputThrottle>(throttle, rules);
        }

        private FuzzyRuleSet<FzOutputWheel> GetWheelRuleSet(FuzzySet<FzOutputWheel> wheel)
        {
            var rules = this.GetWheelRules();
            return new FuzzyRuleSet<FzOutputWheel>(wheel, rules);
        }


        protected override void Awake()
        {
            base.Awake();

            StudentName = "Devanshi Gupta";

            // Only the AI can control. No humans allowed!
            IsPlayer = false;

        }

        protected override void Start()
        {
            base.Start();

            // TODO: You can initialize a bunch of Fuzzy stuff here
            fzSpeedSet = this.GetSpeedSet();

            fzThrottleSet = this.GetThrottleSet();
            fzThrottleRuleSet = this.GetThrottleRuleSet(fzThrottleSet);
            fzVehiclePositionSet = this.GetVehiclePosSet();
            fzVehicleDirectionSet = this.GetVehicleDirSet();

            fzWheelSet = this.GetWheelSet();
            fzWheelRuleSet = this.GetWheelRuleSet(fzWheelSet);

            fzInputValueSet = new FuzzyValueSet();
        }

        System.Text.StringBuilder strBldr = new System.Text.StringBuilder();

        override protected void Update()
        {

            // TODO Do all your Fuzzy stuff here and then
            // pass your fuzzy rule sets to ApplyFuzzyRules()
            

            // Remove these once you get your fuzzy rules working.
            // You can leave one hardcoded while you work on the other.
            // Both steering and throttle must be implemented with variable
            // control and not fixed/hardcoded!

            //HardCodeSteering(0f);
            //HardCodeThrottle(1f);
            Vector3 difference = (transform.position - pathTracker.closestPointOnPath);
            float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(pathTracker.closestPointOnPath.x, pathTracker.closestPointOnPath.z));
            float signed_angle = Vector3.SignedAngle(difference, pathTracker.closestPointDirectionOnPath, Vector3.up);

            // EVAL THROTTLE
            float curr;
            if (signed_angle > 0) {
                curr = distance * -1;
            } else {
                curr = distance;
            }

            fzVehiclePositionSet.Evaluate(curr, fzInputValueSet);            
            fzSpeedSet.Evaluate(Speed,fzInputValueSet);
            var results = fzThrottleRuleSet.Evaluate(fzInputValueSet);
            float crisp = results / 87;
            Throttle = crisp;
            
            // Simple example of fuzzification of vehicle state
            // The Speed is fuzzified and stored in fzInputValueSet
            float vehicleDir = Vector3.SignedAngle(transform.forward, pathTracker.closestPointDirectionOnPath, Vector3.up);
            fzVehicleDirectionSet.Evaluate(vehicleDir, fzInputValueSet);
            fzVehiclePositionSet.Evaluate(curr*1f, fzInputValueSet);
            var results2 = fzWheelRuleSet.Evaluate(fzInputValueSet);
            Steering = results2*1f;
            
            //fzSpeedSet.Evaluate(Speed, fzInputValueSet);

                // ApplyFuzzyRules evaluates your rules and assigns Thottle and Steering accordingly
                // Also, some intermediate values are passed back for debugging purposes
                // Throttle = someValue; //[-1f, 1f] -1 is full brake, 0 is neutral, 1 is full throttle
                // Steering = someValue; // [-1f, 1f] -1 if full left, 0 is neutral, 1 is full right
                

            ApplyFuzzyRules<FzOutputThrottle, FzOutputWheel>(
                    fzThrottleRuleSet,
                    fzWheelRuleSet,
                    fzInputValueSet,
                    // access to intermediate state for debugging
                    out var throttleRuleOutput,
                    out var wheelRuleOutput,
                    ref mergedThrottle,
                    ref mergedWheel
            );

                
                // Use vizText for debugging output
                // You might also use Debug.DrawLine() to draw vectors on Scene view
            /*
            if (vizText != null)
            {
                    strBldr.Clear();

                    strBldr.AppendLine($"Demo Output");
                    strBldr.AppendLine($"Comment out before submission");

                    // You will probably want to selectively enable/disable printing
                    // of certain fuzzy states or rules

                    AIVehicle.DiagnosticPrintFuzzyValueSet<FzInputSpeed>(fzInputValueSet, strBldr);
    
                    AIVehicle.DiagnosticPrintRuleSet<FzOutputThrottle>(fzThrottleRuleSet, throttleRuleOutput, strBldr);
                    AIVehicle.DiagnosticPrintRuleSet<FzOutputWheel>(fzWheelRuleSet, wheelRuleOutput, strBldr);

                    vizText.text = strBldr.ToString();
            }
            */

                // recommend you keep the base Update call at the end, after all your FuzzyVehicle code so that
                // control inputs can be processed properly (e.g. Throttle, Steering)
            base.Update();
            
        }

    }
}
