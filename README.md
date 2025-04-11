# AcrylicDarkUI
Interface personalizada com cantos arredondados e efeito de desfoque (blur) implementada em Windows Forms utilizando C# e chamadas à API do Windows (Win32).

# Blur UI - HighFade

![screenshot](https://i.imgur.com/GXQqJtX.png)

> Interface moderna com cantos arredondados e efeito de desfoque (blur) para Windows Forms.

## ✨ Sobre o Projeto

Este projeto foi criado por mim, **nuzy**, com o objetivo de experimentar e demonstrar como criar uma interface customizada utilizando o `Windows Forms` com:

- Bordas arredondadas
- Desfoque no fundo estilo *Acrylic/Blur Behind*
- Movimento da janela através do mouse (drag-and-drop personalizado)
- Interface sem borda (form borderless)

Ideal para interfaces modernas, launchers ou ferramentas auxiliares no Windows.

---

## 🧰 Tecnologias Utilizadas

- C#
- Windows Forms
- Win32 API (via `DllImport`)
- GDI32 & User32

---

## 🚀 Como Rodar

1. Clone o repositório:
   ```bash
   git clone https://github.com/nuzydev/AcrylicDarkUI.git
   ```

2. Abra o projeto no **Visual Studio**.

3. Compile e execute com o atalho `Ctrl + F5`.

---

## 🧪 Funcionalidades

- [x] Interface arrastável sem borda
- [x] Cantos arredondados customizados
- [x] Efeito de blur dinâmico
- [x] Responsividade na alteração de tamanho

---

## 🔧 Personalização

Você pode alterar facilmente:

- A cor de fundo (linha `BACKGROUND_COLOR`)
- O raio da borda (linha `CORNER_RADIUS`)
- O título da janela (linha `this.Text = "Blur UI";`)

---
