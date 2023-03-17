```
cd ClientApi

dotnet publish -c Release --os linux --arch x64 -p:PublishProfile=DefaultContainer

cd ..

cd ..

kubectl apply -f .\deployment.yaml

kubectl port-forward services/client-api 65532:65531 -n default

```

```
kubectl debug server-service-66bf744488-nbnrr -it --image=nicolaka/netshoot
```