<h1>🛒 API de Gerenciamento de Produtos</h1>

<p>API RESTful em <strong>ASP.NET Core</strong> para gestão de produtos, clientes e pedidos, com foco em regras reais de negócio como controle de estoque, desconto automático e validação de CPF.</p>

---

## 📦 Funcionalidades

- Cadastro e gerenciamento de produtos, clientes e pedidos
- Validações de negócio (estoque, CPF, quantidade, finalização)
- Descontos automáticos para pedidos acima de R$500
- Atualização de estoque ao finalizar pedido
- Cálculo de subtotal e total com desconto

---

## 📋 Regras de Negócio

1. 🚫 Bloqueio de venda com estoque insuficiente  
2. 💰 Desconto de 10% em pedidos acima de R$500  
3. 🧮 Total = Soma dos subtotais - desconto  
4. 📉 Redução de estoque ao finalizar o pedido  
5. 🔄 Subtotal calculado automaticamente  
6. ❌ Bloqueio de quantidade zero ou negativa  
7. 🧾 CPF válido (formato e 11 dígitos)  
8. 🔒 Impedir exclusão de produtos vinculados a pedidos  
9. ✅ Pedido precisa de pelo menos um item  
10. 🔐 Pedido finalizado não pode ser editado

---

## 🚪 Endpoints

### 📦 Produtos
- `GET /produtos`  
- `GET /produtos/{id}`  
- `POST /produtos`  
- `PUT /produtos/{id}`  
- `DELETE /produtos/{id}`

### 👤 Clientes
- `GET /clientes`  
- `GET /clientes/{id}`  
- `POST /clientes`  
- `PUT /clientes/{id}`  
- `DELETE /clientes/{id}`

### 🛒 Pedidos
- `GET /pedidos`  
- `GET /pedidos/{id}`  
- `POST /pedidos`  
- `PUT /pedidos/{id}/finalizar`  
- `DELETE /pedidos/{id}`

### 🧾 Itens do Pedido
- `POST /pedidos/{pedidoId}/itens`  

---

## ▶️ Como Executar

```bash
git clone https://github.com/Felipe-Passos03/3-Api-EcomerceProdutos
cd 3-Api-EcomerceProdutos
dotnet ef database update
dotnet run
