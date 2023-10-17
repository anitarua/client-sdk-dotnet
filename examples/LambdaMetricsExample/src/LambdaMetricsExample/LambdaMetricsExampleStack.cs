using Amazon.CDK;
using Amazon.CDK.AWS.Lambda;
using Amazon.CDK.AWS.IAM;
using Constructs;

namespace LambdaMetricsExample
{
    public class LambdaMetricsExampleStack : Stack
    {
        internal LambdaMetricsExampleStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            // The code that defines your stack goes here

            Function fn = new Function(this, "MomentoMetricsMiddlewareCDKExample", new FunctionProps
            {
                FunctionName = "MomentoMetricsMiddlewareCDKExample",
                Runtime = Runtime.DOTNET_6,
                Code = Code.FromAsset("./MomentoMetricsMiddlewareCDKExample/src/MomentoMetricsMiddlewareCDKExample/bin/Release/net6.0/publish"),
                Handler = "MomentoMetricsMiddlewareCDKExample::MomentoMetricsMiddlewareCDKExample.Function::FunctionHandler",
                Timeout = Duration.Minutes(6)
            });
        }
    }
}
