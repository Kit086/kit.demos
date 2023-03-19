```
// Open in OpenTelemetryDemo/ directory

cd ClientApi

// build docker image for ClientApi
dotnet publish -c Release --os linux --arch x64 -p:PublishProfile=DefaultContainer

cd ..

// build docker image for ServerService
docker build -t kitdemo-opentelemetry/server-service:1.0.0 -f ServerService/Dockerfile .

// run apps in k8s
kubectl apply -f .

// port forward
kubectl port-forward services/client-api 65530:80 -n default
kubectl port-forward services/jaeger 16686:16686 -n default

// curl web api
curl http://localhost:65530/order/KitLau/orders
```

network debug:

```
kubectl debug server-service-66bf744488-nbnrr -it --image=nicolaka/netshoot
```