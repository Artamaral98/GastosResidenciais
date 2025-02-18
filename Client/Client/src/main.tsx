import { createRoot } from 'react-dom/client'
import { BrowserRouter } from 'react-router-dom'
import './index.css'
import App from './App.tsx'
import { Toaster } from 'react-hot-toast'

createRoot(document.getElementById('root')!).render(
   <BrowserRouter>
    <App />
    <Toaster toastOptions={{
            position: 'top-right',
            style: {
                background: '#283046',
                color: 'white'
            }
    }} />
   </BrowserRouter>,
)
