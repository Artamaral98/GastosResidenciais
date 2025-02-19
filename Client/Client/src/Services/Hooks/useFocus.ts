import { useRef, useEffect } from "react";

//Faz com que o cursor aponte automáticamente para um input
const useFocus = () => {
    const inputRef = useRef<HTMLInputElement>(null);


    const setFocus = () => {
        if (inputRef.current) {
            inputRef.current.focus();
        }
    };

    useEffect(() => {
        setFocus();
    }, []);

    return { inputRef, setFocus };
};

export default useFocus;
